using TaskManager.Api.Models;

namespace TaskManager.Api.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity> GetByIdAsync(Guid id);
        Task AddAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task<bool> DeleteAsync(Guid id);
    }
}