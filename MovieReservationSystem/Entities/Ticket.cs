using System.ComponentModel.DataAnnotations;

namespace MovieReservationSystem.Entities
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public int SeatId { get; set; }
        public int ScreeningId { get; set; } 

        public Seat Seat { get; set; }
        public Screening Screening { get; set; } 

    }
}
