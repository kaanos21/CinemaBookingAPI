namespace MovieReservationSystem.Dtos.MovieDtos
{
    public class GetByIdMovieDto
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
    }
}
