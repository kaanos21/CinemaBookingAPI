using MovieReservationSystem.Dtos.CategoryDtos;

namespace MovieReservationSystem.Dtos.MovieDtos
{
    public class CreateMovieDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public ICollection<int> Categories { get; set; }
    }
}
