using TaskManagement.Domain.DTO;
using TaskManagement.Domain;

namespace TaskManagement.Domain.Services;

public interface IToDoTaskService
{
    Task<ServiceReponse<List<ToDoTaskDto>>> GetAllTasksAsync();
    Task<ServiceReponse<ToDoTaskDto>> GetTaskByIdAsync(int id);
    Task<ServiceReponse<int>> CreateTaskAsync(CreateToDoTaskDto ToDotaskDto);
    Task<ServiceReponse<bool>> UpdateTaskAsync(int id, UpdateToDoTaskDto ToDotaskDto);
    Task<ServiceReponse<bool>> DeleteTaskAsync(int id);
}
