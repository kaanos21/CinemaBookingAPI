using MovieReservationSystem.Dtos.CategoryDtos;

namespace MovieReservationSystem.Dtos.MovieDtos
{
    public class GetAllMovieWithCategoriesDto
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
    }
}
