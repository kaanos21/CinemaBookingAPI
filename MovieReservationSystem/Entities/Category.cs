using System.ComponentModel.DataAnnotations;

namespace MovieReservationSystem.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
