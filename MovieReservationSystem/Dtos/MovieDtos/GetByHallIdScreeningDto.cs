namespace MovieReservationSystem.Dtos.MovieDtos
{
    public class GetByHallIdScreeningDto
    {
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string HallName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
