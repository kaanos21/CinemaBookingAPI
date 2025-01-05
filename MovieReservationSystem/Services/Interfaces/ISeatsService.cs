using MovieReservationSystem.Dtos.SeatDtos;

namespace MovieReservationSystem.Services.Interfaces
{
    public interface ISeatsService
    {
        Task<List<AvaibleSeatsDto>> GetByScreeningIdAvaibleSeats(int id);
    }
}
