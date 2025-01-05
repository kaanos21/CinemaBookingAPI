namespace MovieReservationSystem.Dtos.TicketDtos
{
    public class BuyTicketDto
    {
        public int ScreeningId { get; set; } 
        public List<PersonTicketInfo> Persons { get; set; } 
    }
}