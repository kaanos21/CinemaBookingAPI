namespace MovieReservationSystem.Dtos.ScreeningDtos
{
    public class GetByIdScreeningDetailDto
    {
        public int ScreeningId { get; set; }
        public string MovieName { get; set; } 
        public string HallName { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
