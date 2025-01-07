using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain;
using TaskManagement.Domain.Models;
using TaskManagement.API.Data;
using TaskManagement.Domain.DTO;
using TaskManagement.Domain.Services;

namespace TaskManagement.API.Services;

public class CategoryService : ICategoryService
{
    private readonly TaskManagementDbContext _context;

    public CategoryService(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceReponse<List<CategoryDto>>> GetAllCategoriesAsync()
    {
        var response = new ServiceReponse<List<CategoryDto>>();
        try
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    DueDate = c.DueDate,
                    Priority = c.Priority
                })
                .ToListAsync();

            response.Data = categories;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error retrieving categories: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<CategoryDto>> GetCategoryByIdAsync(int id)
    {
        var response = new ServiceReponse<CategoryDto>();
        try
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                response.Success = false;
                response.Message = $"Category with id {id} not found.";
                return response;
            }

            response.Data = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                DueDate = category.DueDate,
                Priority = category.Priority
            };
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error retrieving category: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<int>> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var response = new ServiceReponse<int>();
        try
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                DueDate = categoryDto.DueDate,
                Priority = categoryDto.Priority
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            response.Data = category.Id;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error creating category: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<bool>> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
    {
        var response = new ServiceReponse<bool>();
        try
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                response.Success = false;
                response.Message = $"Category with id {id} not found.";
                return response;
            }

            category.Name = categoryDto.Name;
            category.DueDate = categoryDto.DueDate;
            category.Priority = categoryDto.Priority;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            response.Data = true;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error updating category: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<bool>> DeleteCategoryAsync(int id)
    {
        var response = new ServiceReponse<bool>();
        try
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                response.Success = false;
                response.Message = $"Category with id {id} not found.";
                return response;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            response.Data = true;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error deleting category: {ex.Message}";
        }
        return response;
    }
}
