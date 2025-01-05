using System.ComponentModel.DataAnnotations;

namespace MovieReservationSystem.Entities
{
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }
        public int ScreeningId { get; set; }
        public int Number { get; set; }
        public bool IsReserved { get; set; }

        public Screening Screening { get; set; }
        public Ticket Ticket { get; set; }

    }
}
