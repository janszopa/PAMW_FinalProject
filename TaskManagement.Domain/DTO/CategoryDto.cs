namespace TaskManagement.Domain.DTO;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
}
