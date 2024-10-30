using TaskManager.Api.Models;

namespace TaskManager.Api.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(Guid id);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> HasPendingTasksAsync(Guid personId);
        
    }
}