using TaskManagement.Domain.DTO;
using TaskManagement.Domain;

namespace TaskManagement.Domain.Services;

public interface ICategoryService
{
    Task<ServiceReponse<List<CategoryDto>>> GetAllCategoriesAsync();
    Task<ServiceReponse<CategoryDto>> GetCategoryByIdAsync(int id);
    Task<ServiceReponse<int>> CreateCategoryAsync(CreateCategoryDto categoryDto);
    Task<ServiceReponse<bool>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
    Task<ServiceReponse<bool>> DeleteCategoryAsync(int id);
}

