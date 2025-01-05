using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Concrete;
using MovieReservationSystem.Dtos.HallDtos;
using MovieReservationSystem.Entities;
using MovieReservationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReservationSystem.Services.Repository
{
    public class HallService : IHallService
    {
        private readonly Context _context;

        public HallService(Context context)
        {
            _context = context;
        }

        public async Task CreateHall(CreateHallDto createHallDto)
        {
            try
            {
                Hall hall = new Hall()
                {
                    Name = createHallDto.Name,
                    Capacity = createHallDto.Capacity,
                    IsActive=true,
                };

                await _context.Halls.AddAsync(hall);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating the hall.", ex);
            }
        }

        public async Task DeleteHall(int id)
        {
            try
            {
                var hall = await _context.Halls.FindAsync(id);
                if (hall == null)
                {
                    throw new KeyNotFoundException($"Hall with ID {id} not found.");
                }

                hall.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deactivating the hall.", ex);
            }
        }

        public async Task UpdateHall(UpdateHallDto updateHallDto)
        {
            try
            {
                var hall = await _context.Halls.FindAsync(updateHallDto.HallId);
                if (hall == null)
                {
                    throw new Exception("Hall not found.");
                }

                hall.Name = updateHallDto.Name;
                hall.Capacity = updateHallDto.Capacity;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating the hall.", ex);
            }
        }

        public async Task<GetByIdHallDto> GetByIdHall(int id)
        {
            try
            {
                var hall = await _context.Halls
                    .Where(h => h.IsActive==true) 
                    .FirstOrDefaultAsync(h => h.HallId == id);

                if (hall == null)
                {
                    throw new Exception("Hall not found.");
                }

                return new GetByIdHallDto
                {
                    HallId = hall.HallId,
                    Name = hall.Name,
                    Capacity = hall.Capacity,
                    IsActive = hall.IsActive
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching the hall details.", ex);
            }
        }

        public async Task<List<GetAllHallDto>> GetAllHalls()
        {
            try
            {
                var halls = await _context.Halls
                    .Where (h => h.IsActive==true)
                    .Select(h => new GetAllHallDto
                    {
                        HallId = h.HallId,
                        Name = h.Name,
                        Capacity = h.Capacity
                    })
                    .ToListAsync();

                return halls;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching all halls.", ex);
            }
        }
    }
}