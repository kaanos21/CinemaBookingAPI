using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Concrete;
using MovieReservationSystem.Dtos.SeatDtos;
using MovieReservationSystem.Services.Interfaces;

namespace MovieReservationSystem.Services.Repository
{
    public class SeatsService : ISeatsService
    {
        private readonly Context _context;

        public SeatsService(Context context)
        {
            _context = context;
        }
        public async Task<List<AvaibleSeatsDto>> GetByScreeningIdAvaibleSeats(int id)
        {
            try
            {
                var result = await _context.Seats.Where(x => x.IsReserved == false).ToListAsync();
                if (result.Count == 0)
                {
                    throw new Exception("No seats avaible");
                }

                var seats = result.Select(s => new AvaibleSeatsDto
                {
                    Number = s.Number,
                }).ToList();

                return seats;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while finding avaible seats");
            }

        }
    }
}
