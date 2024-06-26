using Moq;
using TaskManagement.Contracts.Repositories;
using TaskModel = TaskManagement.Models.Task;

namespace TaskManagement.Services.Tests
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockTaskRepository.Object);
        }

        [Fact]
        public async Task GetAllTasksAsync_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<TaskModel>
            {
                new TaskModel { Id = 1, Title = "Test Task 1", Description = "Description 1", Priority = 1, DueDate = DateTime.Now, Status = "Open" },
                new TaskModel { Id = 2, Title = "Test Task 2", Description = "Description 2", Priority = 2, DueDate = DateTime.Now, Status = "Open" }
            };

            _mockTaskRepository.Setup(repo => repo.GetAllTasksAsync()).ReturnsAsync((IEnumerable<TaskModel>)tasks);

            // Act
            var result = await _taskService.GetAllTasksAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal("Test Task 1", resultList[0].Title);
            Assert.Equal("Test Task 2", resultList[1].Title);
        }

        [Fact]
        public async Task GetTaskByIdAsync_ShouldReturnTask()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Test Task", Description = "Description", Priority = 1, DueDate = DateTime.Now, Status = "Open" };

            _mockTaskRepository.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync(task);

            // Act
            var result = await _taskService.GetTaskByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Task", result.Title);
        }

        [Fact]
        public async Task AddTaskAsync_ShouldAddTask()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Test Task", Description = "Description", Priority = 1, DueDate = DateTime.Now, Status = "Open" };

            _mockTaskRepository.Setup(repo => repo.AddTaskAsync(task)).Returns(Task.CompletedTask);

            // Act
            await _taskService.AddTaskAsync(task);

            // Assert
            _mockTaskRepository.Verify(repo => repo.AddTaskAsync(task), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldUpdateTask()
        {
            // Arrange
            var task = new TaskModel { Id = 1, Title = "Test Task", Description = "Description", Priority = 1, DueDate = DateTime.Now, Status = "Open" };

            _mockTaskRepository.Setup(repo => repo.UpdateTaskAsync(task)).Returns(Task.CompletedTask);

            // Act
            await _taskService.UpdateTaskAsync(task);

            // Assert
            _mockTaskRepository.Verify(repo => repo.UpdateTaskAsync(task), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldDeleteTask()
        {
            // Arrange
            _mockTaskRepository.Setup(repo => repo.DeleteTaskAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _taskService.DeleteTaskAsync(1);

            // Assert
            _mockTaskRepository.Verify(repo => repo.DeleteTaskAsync(1), Times.Once);
        }
    }
}