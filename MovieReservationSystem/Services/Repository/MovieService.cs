using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Concrete;
using MovieReservationSystem.Dtos.CategoryDtos;
using MovieReservationSystem.Dtos.MovieDtos;
using MovieReservationSystem.Entities;
using MovieReservationSystem.Services.Interfaces;

namespace MovieReservationSystem.Services.Repository
{
    public class MovieService : IMovieService
    {
        private readonly Context _context;

        public MovieService(Context context)
        {
            _context = context;
        }

        public async Task CreateMovie(CreateMovieDto createMovieDto)
        {
            try
            {
                if (createMovieDto.Categories == null || !createMovieDto.Categories.Any())
                {
                    throw new Exception("At least one category must be provided.");
                }

                var movie = new Movie()
                {
                    Name = createMovieDto.Name,
                    Description = createMovieDto.Description,
                    DurationMinutes = createMovieDto.DurationMinutes,
                    Categories = new List<Category>()
                };

                var categories = await _context.Categories
                    .Where(c => createMovieDto.Categories.Contains(c.CategoryId))
                    .ToListAsync();

                if (!categories.Any())
                {
                    throw new Exception("None of the provided categories exist.");
                }

                foreach (var category in categories)
                {
                    movie.Categories.Add(category);
                }

                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie != null)
                {
                    _context.Movies.Remove(movie);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error while deleting the movie.");
            }
        }

        public async Task<GetByIdMovieDto> GetByIdMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null)
                {
                    throw new Exception("Movie not found.");
                }

                return new GetByIdMovieDto()
                {
                    Name = movie.Name,
                    Description = movie.Description,
                    DurationMinutes = movie.DurationMinutes,
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while fetching the movie details.");
            }
        }

        public async Task UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            try
            {
                var movie = await _context.Movies
                    .Include(m => m.Categories) // Mevcut kategorileri dahil et
                    .FirstOrDefaultAsync(m => m.MovieId == updateMovieDto.MovieId);

                if (movie == null)
                {
                    throw new Exception("Movie not found.");
                }

                // Film bilgilerini güncelle
                movie.Name = updateMovieDto.Name;
                movie.Description = updateMovieDto.Description;
                movie.DurationMinutes = updateMovieDto.DurationMinutes;

                // Kategori kimliklerinin sağlanıp sağlanmadığını kontrol et
                if (updateMovieDto.Categories == null || !updateMovieDto.Categories.Any())
                {
                    throw new Exception("At least one category must be provided.");
                }

                // Mevcut kategorileri temizle
                movie.Categories.Clear();

                // Yeni kategorileri ekle
                var categories = await _context.Categories
                    .Where(c => updateMovieDto.Categories.Contains(c.CategoryId))
                    .ToListAsync();

                // Eğer belirtilen kategori kimliklerinden hiçbiri veritabanında yoksa hata fırlat
                if (!categories.Any())
                {
                    throw new Exception("None of the provided categories exist.");
                }

                foreach (var category in categories)
                {
                    movie.Categories.Add(category);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata loglama yapılabilir
                // _logger.LogError(ex, "Error while updating the movie.");
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GetAllMovieDto>> GetAllMovies()
        {
            try
            {
                var movies = await _context.Movies.ToListAsync();
                return movies.Select(movie => new GetAllMovieDto
                {
                    MovieId = movie.MovieId,
                    Name = movie.Name,
                    Description = movie.Description,
                    DurationMinutes = movie.DurationMinutes
                }).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error while fetching all movies.");
            }
        }
        public async Task<List<GetAllMovieWithCategoriesDto>> GetAllMoviesWithCategories()
        {
            try
            {
                var movies = await _context.Movies
                    .Include(m => m.Categories)
                    .ToListAsync();

                return movies.Select(movie => new GetAllMovieWithCategoriesDto
                {
                    MovieId = movie.MovieId,
                    Name = movie.Name,
                    Description = movie.Description,
                    DurationMinutes = movie.DurationMinutes,
                    Categories = movie.Categories
                        .Where(c => c.Active == true)
                        .Select(c => new CategoryDto
                        {
                            CategoryId = c.CategoryId,
                            Name = c.Name
                        }).ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching movies with categories.");
            }
        }
    }
}
