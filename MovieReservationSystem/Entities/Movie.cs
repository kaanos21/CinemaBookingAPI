using System.ComponentModel.DataAnnotations;

namespace MovieReservationSystem.Entities
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        public int DurationMinutes { get; set; } 

        public ICollection<Category> Categories { get; set; } 
        public ICollection<Screening> Screenings { get; set; }
    }
}
