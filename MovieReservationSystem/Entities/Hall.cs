using System.ComponentModel.DataAnnotations;

namespace MovieReservationSystem.Entities
{
    public class Hall
    {
        [Key]
        public int HallId { get; set; } 
        public string Name { get; set; } 
        public int Capacity { get; set; } 
        public bool IsActive { get; set; }
        public ICollection<Seat> Seats { get; set; } 
        public ICollection<Screening> Screenings { get; set; } 
    }
}
