namespace TaskManagement.Domain.DTO;

public class UpdateCategoryDto
{
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
}
