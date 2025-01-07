using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Models;
using TaskManagement.Domain;
using TaskManagement.API.Data;
using TaskManagement.Domain.DTO;
using TaskManagement.Domain.Services;

namespace TaskManagement.API.Services;

public class ToDoTaskService : IToDoTaskService
{
    private readonly TaskManagementDbContext _context;

    public ToDoTaskService(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceReponse<List<ToDoTaskDto>>> GetAllTasksAsync()
    {
        var response = new ServiceReponse<List<ToDoTaskDto>>();
        try
        {
            var ToDotasks = await _context.ToDoTasks
                .Include(t => t.Person)
                .Include(t => t.Category)
                .Select(t => new ToDoTaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted,
                    PersonId = t.PersonId,
                    CategoryId = t.CategoryId
                })
                .ToListAsync();

            response.Data = ToDotasks;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error retrieving ToDotasks: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<ToDoTaskDto>> GetTaskByIdAsync(int id)
    {
        var response = new ServiceReponse<ToDoTaskDto>();
        try
        {
            var task = await _context.ToDoTasks
                .Include(t => t.Person)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                response.Success = false;
                response.Message = $"Task with id {id} not found.";
                return response;
            }

            response.Data = new ToDoTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                PersonId = task.PersonId,
                CategoryId = task.CategoryId
            };
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error retrieving task: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<int>> CreateTaskAsync(CreateToDoTaskDto taskDto)
    {
        var response = new ServiceReponse<int>();
        try
        {
            var task = new ToDoTask
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                IsCompleted = false,
                PersonId = taskDto.PersonId,
                CategoryId = taskDto.CategoryId
            };

            _context.ToDoTasks.Add(task);
            await _context.SaveChangesAsync();
            response.Data = task.Id;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error creating task: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<bool>> UpdateTaskAsync(int id, UpdateToDoTaskDto taskDto)
    {
        var response = new ServiceReponse<bool>();
        try
        {
            var task = await _context.ToDoTasks.FindAsync(id);

            if (task == null)
            {
                response.Success = false;
                response.Message = $"Task with id {id} not found.";
                return response;
            }

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.IsCompleted = taskDto.IsCompleted;

            _context.ToDoTasks.Update(task);
            await _context.SaveChangesAsync();
            response.Data = true;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error updating task: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceReponse<bool>> DeleteTaskAsync(int id)
    {
        var response = new ServiceReponse<bool>();
        try
        {
            var task = await _context.ToDoTasks.FindAsync(id);

            if (task == null)
            {
                response.Success = false;
                response.Message = $"Task with id {id} not found.";
                return response;
            }

            _context.ToDoTasks.Remove(task);
            await _context.SaveChangesAsync();
            response.Data = true;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error deleting task: {ex.Message}";
        }
        return response;
    }
}
