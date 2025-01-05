using MovieReservationSystem.Dtos.MovieDtos;

namespace MovieReservationSystem.Services.Interfaces
{
    public interface IMovieService
    {
        Task CreateMovie(CreateMovieDto createMovieDto);
        Task DeleteMovie(int id);
        Task UpdateMovie(UpdateMovieDto updateMovieDto);
        Task<GetByIdMovieDto> GetByIdMovie(int id);
        Task<List<GetAllMovieDto>> GetAllMovies();
        Task<List<GetAllMovieWithCategoriesDto>> GetAllMoviesWithCategories();
    }
}
