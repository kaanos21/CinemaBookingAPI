using MovieReservationSystem.Dtos.HallDtos;

namespace MovieReservationSystem.Services.Interfaces
{
    public interface IHallService
    {
        Task CreateHall(CreateHallDto createHallDto);
        Task DeleteHall(int id);
        Task UpdateHall(UpdateHallDto updateHallDto);
        Task<GetByIdHallDto> GetByIdHall(int id);
        Task<List<GetAllHallDto>> GetAllHalls();
    }
}
