using MovieReservationSystem.Dtos.TicketDtos;

namespace MovieReservationSystem.Services.Interfaces
{
    public interface ITicketService
    {
        Task BuyTicket(int userId,BuyTicketDto buyTicketDto);
    }
}
