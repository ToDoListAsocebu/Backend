using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Interfaces.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(int id);
    Task<TaskItem?> GetByTitleAsync(string title);
}
