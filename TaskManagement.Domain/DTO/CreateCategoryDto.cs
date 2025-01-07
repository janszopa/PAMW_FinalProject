namespace TaskManagement.Domain.DTO;

public class CreateCategoryDto
{
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
}
