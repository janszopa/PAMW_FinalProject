namespace TaskManagement.Domain.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; } // Priorytet, np. low, medium, high
    public virtual ICollection<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();
}
