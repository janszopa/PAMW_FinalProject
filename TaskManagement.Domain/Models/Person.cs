namespace TaskManagement.Domain.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();
}
