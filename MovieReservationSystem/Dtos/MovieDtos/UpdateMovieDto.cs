namespace MovieReservationSystem.Dtos.MovieDtos
{
    public class UpdateMovieDto
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public ICollection<int> Categories { get; set; } // Kategori kimliklerini içeren koleksiyon
    }
}