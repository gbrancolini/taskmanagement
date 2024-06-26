using Microsoft.EntityFrameworkCore;
using TaskModel = TaskManagement.Models.Task;

namespace TaskManagement.Repositories.Tests
{

    public class TaskRepositoryTests
    {
        private readonly TaskDbContext _context;
        private readonly TaskRepository _taskRepository;

        public TaskRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskDatabase")
                .Options;

            _context = new TaskDbContext(options);
            _taskRepository = new TaskRepository(_context);
        }

        private void ResetDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            // Arrange
            ResetDatabase();

            var tasks = new List<TaskModel>
            {
                new TaskModel { Id = 1, Title = "Test Task 1", Description = "Description 1", Priority = 1, DueDate = System.DateTime.Now, Status = "Open" },
                new TaskModel{ Id = 2, Title = "Test Task 2", Description = "Description 2", Priority = 2, DueDate = System.DateTime.Now, Status = "Open" }
            };

            _context.Tasks.AddRange(tasks);
            _context.SaveChanges();

            // Act
            var result = await _taskRepository.GetAllTasksAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTaskByIdAsync_ShouldReturnTask()
        {
            // Arrange
            ResetDatabase();

            var task = new TaskModel { Id = 1, Title = "Test Task", Description = "Description", Priority = 1, DueDate = System.DateTime.Now, Status = "Open" };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            // Act
            var result = await _taskRepository.GetTaskByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddTaskAsync_ShouldAddTask()
        {
            // Arrange
            ResetDatabase();

            var task = new TaskModel { Id = 1, Title = "Test Task", Description = "Description", Priority = 1, DueDate = System.DateTime.Now, Status = "Open" };

            // Act
            await _taskRepository.AddTaskAsync(task);
            var result = await _context.Tasks.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldUpdateTask()
        {
            // Arrange
            ResetDatabase();

            var task = new TaskModel { Id = 1, Title = "Test Task", Description = "Description", Priority = 1, DueDate = System.DateTime.Now, Status = "Open" };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            task.Title = "Updated Task";

            // Act
            await _taskRepository.UpdateTaskAsync(task);
            var result = await _context.Tasks.FindAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Task", result.Title);
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldRemoveTask()
        {
            // Arrange
            ResetDatabase();

            var task = new TaskModel { Id = 1, Title = "Test Task", Description = "Description", Priority = 1, DueDate = System.DateTime.Now, Status = "Open" };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            // Act
            await _taskRepository.DeleteTaskAsync(1);
            var result = await _context.Tasks.FindAsync(1);

            // Assert
            Assert.Null(result);
        }
    }
}
