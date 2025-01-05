namespace MovieReservationSystem.Dtos.CategoryDtos
{
    public class GetByIdCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
