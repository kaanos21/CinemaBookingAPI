namespace MovieReservationSystem.Dtos.ScreeningDtos
{
    public class GetByIdScreeningDto
    {
        public int ScreeningId { get; set; }
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
