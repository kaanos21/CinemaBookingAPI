using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Concrete;
using MovieReservationSystem.Dtos.MovieDtos;
using MovieReservationSystem.Dtos.ScreeningDtos;
using MovieReservationSystem.Entities;
using MovieReservationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieReservationSystem.Services.Repository
{
    public class ScreeningService : IScreeningService
    {
        private readonly Context _context;

        public ScreeningService(Context context)
        {
            _context = context;
        }

        public async Task CreateScreening(CreateScreeningDto createScreeningDto)
        {
            try
            {
                var screening = new Screening
                {
                    HallId = createScreeningDto.HallId,
                    MovieId = createScreeningDto.MovieId,
                    StartTime = DateTime.SpecifyKind(createScreeningDto.StartTime, DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(createScreeningDto.EndTime, DateTimeKind.Utc),
                    Seats = new List<Seat>()
                };

                for (int i = 1; i <= 20; i++)
                {
                    screening.Seats.Add(new Seat
                    {
                        
                        Number = i,
                        IsReserved = false
                    });
                }

                await _context.Screenings.AddAsync(screening);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating the screening.", ex);
            }
        }

        public async Task DeleteScreening(int id)
        {
            try
            {
                var screening = await _context.Screenings.FindAsync(id);
                if (screening == null)
                {
                    throw new KeyNotFoundException($"Screening with ID {id} not found.");
                }

                _context.Screenings.Remove(screening);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the screening.", ex);
            }
        }

        public async Task UpdateScreening(UpdateScreeningDto updateScreeningDto)
        {
            try
            {
                var screening = await _context.Screenings.FindAsync(updateScreeningDto.ScreeningId);
                if (screening == null)
                {
                    throw new Exception("Screening not found.");
                }

                screening.HallId = updateScreeningDto.HallId;
                screening.MovieId = updateScreeningDto.MovieId;
                screening.StartTime = updateScreeningDto.StartTime;
                screening.EndTime = updateScreeningDto.EndTime;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating the screening.", ex);
            }
        }

        public async Task<GetByIdScreeningDto> GetByIdScreening(int id)
        {
            try
            {
                var screening = await _context.Screenings
                    .Include(s => s.Hall)
                    .Include(s => s.Movie)
                    .FirstOrDefaultAsync(s => s.ScreeningId == id);

                if (screening == null)
                {
                    throw new Exception("Screening not found.");
                }

                return new GetByIdScreeningDto
                {
                    ScreeningId = screening.ScreeningId,
                    HallId = screening.HallId,
                    MovieId = screening.MovieId,
                    StartTime = screening.StartTime,
                    EndTime = screening.EndTime
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching the screening details.", ex);
            }
        }

        public async Task<List<GetAllScreeningDto>> GetAllScreenings()
        {
            try
            {
                var screenings = await _context.Screenings
                    .Include(s => s.Hall)
                    .Include(s => s.Movie)
                    .Select(s => new GetAllScreeningDto
                    {
                        ScreeningId = s.ScreeningId,
                        HallId = s.HallId,
                        MovieId = s.MovieId,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime
                    })
                    .ToListAsync();

                return screenings;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching all screenings.", ex);
            }
        }

        public async Task<GetByIdScreeningDetailDto> GetByIdScreeningDetail(int id)
        {
            try
            {
                var screening = await _context.Screenings
                    .Include(s => s.Hall)
                    .Include(s => s.Movie)
                    .FirstOrDefaultAsync(s => s.ScreeningId == id);

                if (screening == null)
                {
                    throw new Exception("Screening not found.");
                }

                return new GetByIdScreeningDetailDto
                {
                    ScreeningId = screening.ScreeningId,
                    MovieName = screening.Movie.Name,
                    HallName = screening.Hall.Name,
                    StartTime = screening.StartTime,
                    EndTime = screening.EndTime
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching the screening details.", ex);
            }
        }

        public async Task<List<GetByMovieIdScreeningDto>> GetByMovieIdScreenings(int id)
        {
            try
            {
                var movielist = await _context.Screenings
                    .Include(m => m.Movie)
                    .Include(h => h.Hall)
                    .Where(s=>s.MovieId== id)
                    .ToListAsync();

                if(movielist == null)
                {
                    throw new Exception("Movie is not found");
                }

                var screeningDtos=movielist.Select(s=> new GetByMovieIdScreeningDto
                {
                    HallId = s.HallId,
                    MovieName = s.Movie.Name,
                    StartTime= s.StartTime,
                    EndTime= s.EndTime,
                    HallName= s.Hall.Name,
                    MovieId=s.MovieId,
                }).ToList();

                return screeningDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching the movie details.", ex);
            }
        }

        public async Task<List<GetByHallIdScreeningDto>> GetByHallIdScreenings(int id)
        {
            try
            {
                var movielist = await _context.Screenings
                    .Include(m => m.Movie)
                    .Include(h => h.Hall)
                    .Where(s => s.HallId == id)
                    .ToListAsync();

                if (movielist == null)
                {
                    throw new Exception("Movie is not found");
                }

                var screeningDtos = movielist.Select(s => new GetByHallIdScreeningDto
                {
                    HallId = s.HallId,
                    MovieName = s.Movie.Name,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    HallName = s.Hall.Name,
                    MovieId = s.MovieId,
                }).ToList();

                return screeningDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching the movie details.", ex);
            }
        }
    }
}