using MovieReservationSystem.Dtos.CategoryDtos;

namespace MovieReservationSystem.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategory(CreateCategoryDto createCategoryDto);
        Task DeleteCategory(int id);
        Task UpdateCategory(UpdateCategoryDto updateCategoryDto);
        Task ReActiveCategory(ReActiveCategoryDto reactiveCategoryDto);
        Task<GetByIdCategoryDto> GetByIdCategory(int id);
        Task<List<GetAllCategoryDto>> GetAllCategories();
        Task<List<GetActiveCategoryDto>> GetActiveCategories();

    }
}
