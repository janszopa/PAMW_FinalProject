using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Models;

namespace TaskManagement.API.Data;

public class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }

    public DbSet<ToDoTask> ToDoTasks { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoTask>()
            .HasOne(t => t.Person)
            .WithMany(p => p.ToDoTasks)
            .HasForeignKey(t => t.PersonId);

        modelBuilder.Entity<ToDoTask>()
            .HasOne(t => t.Category)
            .WithMany(c => c.ToDoTasks)
            .HasForeignKey(t => t.CategoryId);
    }
}
