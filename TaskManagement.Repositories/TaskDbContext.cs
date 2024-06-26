using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Repositories
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Models.Task> Tasks { get; set; }

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
