namespace MovieReservationSystem.Dtos.HallDtos
{
    public class GetByIdHallDto
    {
        public int HallId { get; set; } 
        public string Name { get; set; } 
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
    }
}
