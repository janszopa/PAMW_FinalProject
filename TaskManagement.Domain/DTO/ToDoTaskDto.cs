namespace TaskManagement.Domain.DTO;

public class ToDoTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int PersonId { get; set; }
    public int CategoryId { get; set; }
}
