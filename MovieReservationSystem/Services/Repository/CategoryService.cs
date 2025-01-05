using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Concrete;
using MovieReservationSystem.Dtos.CategoryDtos;
using MovieReservationSystem.Entities;
using MovieReservationSystem.Services.Interfaces;

namespace MovieReservationSystem.Services.Repository
{
    public class CategoryService : ICategoryService
    {
        private readonly Context _context;

        public CategoryService(Context context)
        {
            _context = context;
        }

        public async Task CreateCategory(CreateCategoryDto createCategoryDto)
        {
            try
            {

                Category category = new Category()
                {
                    Name = createCategoryDto.Name,
                    Active = true,
                };


                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                var value = await _context.Categories.FindAsync(id); // Asenkron FindAsync kullanımı.
                if (value == null)
                {
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                }

                value.Active = false;
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException ex)
            {
                throw; // Özel bir durum olduğu için direkt dışarı atıyoruz.
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the category.", ex);
            }
        }

        public async Task<List<GetAllCategoryDto>> GetAllCategories()
        {
            try
            {
                var categories = await _context.Categories
                    .Select(c => new GetAllCategoryDto
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name
                    })
                    .ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching all categories", ex);
            }
        }

        public async Task<List<GetActiveCategoryDto>> GetActiveCategories()
        {
            try
            {
                var activeCategories = await _context.Categories
                    .Where(c => c.Active)
                    .Select(c => new GetActiveCategoryDto
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name
                    })
                    .ToListAsync();

                return activeCategories;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching active categories", ex);
            }
        }

        public async Task<GetByIdCategoryDto> GetByIdCategory(int id)
        {
            try
            {
                var value = await _context.Categories.FindAsync(id);
                if (value == null)
                {
                    throw new Exception("Category not found");
                }

                GetByIdCategoryDto getByIdCategoryDto = new GetByIdCategoryDto()
                {
                    Active = value.Active,
                    CategoryId = id,
                    Name = value.Name,
                };
                return getByIdCategoryDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ReActiveCategory(ReActiveCategoryDto reactiveCategoryDto)
        {
            try
            {
                var value = _context.Categories.Find(reactiveCategoryDto.CategoryId);
                value.Active = true;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }

        public async Task UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var value = await _context.Categories.FindAsync(updateCategoryDto.CategoryId);
                value.Name = updateCategoryDto.Name;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }
    }
}
