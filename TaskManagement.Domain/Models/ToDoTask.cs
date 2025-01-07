namespace TaskManagement.Domain.Models;

public class ToDoTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int PersonId { get; set; }
    public virtual Person Person { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
