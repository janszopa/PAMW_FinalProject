namespace TaskManagement.Domain.DTO;

public class UpdateToDoTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}