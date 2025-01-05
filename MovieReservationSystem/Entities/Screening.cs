using System.ComponentModel.DataAnnotations;

namespace MovieReservationSystem.Entities
{
    public class Screening
    {
        [Key]
        public int ScreeningId { get; set; }
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Hall Hall { get; set; }
        public Movie Movie { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Seat> Seats { get; set; } // Seats koleksiyonu
    }
}
