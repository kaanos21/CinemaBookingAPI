using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Concrete;
using MovieReservationSystem.Dtos.TicketDtos;
using MovieReservationSystem.Entities;
using MovieReservationSystem.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReservationSystem.Services.Repository
{
    public class TicketService : ITicketService
    {
        private readonly Context _context;

        public TicketService(Context context)
        {
            _context = context;
        }

        public async Task BuyTicket(int userId, BuyTicketDto buyTicketDto)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                double totalCost = buyTicketDto.Persons.Count * 200; 

                if (user.Money < totalCost)
                {
                    throw new Exception("Insufficient funds.");
                }

                var screening = await _context.Screenings
                    .Include(s => s.Seats)
                    .FirstOrDefaultAsync(s => s.ScreeningId == buyTicketDto.ScreeningId);

                if (screening == null)
                {
                    throw new Exception("Screening not found.");
                }

                foreach (var person in buyTicketDto.Persons)
                {
                    var seat = screening.Seats.FirstOrDefault(s => s.Number == person.SeatNumber && !s.IsReserved);
                    if (seat == null)
                    {
                        throw new Exception($"Seat number {person.SeatNumber} for screening {buyTicketDto.ScreeningId} is not available.");
                    }

                    seat.IsReserved = true;

                    var ticket = new Ticket
                    {
                        UserId = userId,
                        CustomerName = person.CustomerName,
                        CustomerSurname = person.CustomerSurname,
                        SeatId = seat.SeatId,
                        ScreeningId = buyTicketDto.ScreeningId
                    };

                    await _context.Tickets.AddAsync(ticket);
                }

                user.Money -= totalCost; 
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while purchasing tickets.", ex);
            }
        }
    }
}