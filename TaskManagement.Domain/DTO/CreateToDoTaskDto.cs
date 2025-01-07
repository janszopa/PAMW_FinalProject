namespace TaskManagement.Domain.DTO;

public class CreateToDoTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int PersonId { get; set; }
    public int CategoryId { get; set; }
}