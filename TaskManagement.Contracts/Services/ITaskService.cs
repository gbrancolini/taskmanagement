﻿using TaskModel = TaskManagement.Models.Task;

namespace TaskManagement.Contracts.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAllTasksAsync();
        Task<TaskModel> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskModel task);
        Task UpdateTaskAsync(TaskModel task);
        Task DeleteTaskAsync(int id);
    }
}
