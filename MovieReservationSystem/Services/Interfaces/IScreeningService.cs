using MovieReservationSystem.Dtos.MovieDtos;
using MovieReservationSystem.Dtos.ScreeningDtos;

namespace MovieReservationSystem.Services.Interfaces
{
    public interface IScreeningService
    {
        Task CreateScreening(CreateScreeningDto createScreeningDto);
        Task DeleteScreening(int id);
        Task UpdateScreening(UpdateScreeningDto updateScreeningDto);
        Task<GetByIdScreeningDto> GetByIdScreening(int id);
        Task<List<GetAllScreeningDto>> GetAllScreenings();
        Task<GetByIdScreeningDetailDto> GetByIdScreeningDetail(int id);
        Task<List<GetByMovieIdScreeningDto>> GetByMovieIdScreenings(int id);
        Task<List<GetByHallIdScreeningDto>> GetByHallIdScreenings(int id);
        
    }
}
