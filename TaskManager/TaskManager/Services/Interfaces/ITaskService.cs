

using TaskManager.Api.DTOs;

namespace TaskManager.Api.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetAllTasksAsync();
        Task<TaskDTO> GetTaskByIdAsync(Guid id);
        Task<Guid> AddTaskAsync(CreateTaskDTO taskDto);
        Task UpdateTaskAsync(Guid id, CreateTaskDTO taskDto);
        Task<bool> DeleteTaskAsync(Guid id);
        Task<bool> AssignTaskToPersonAsync(Guid taskId, Guid personId);
        Task<bool> UnassignTaskAsync(Guid taskId);
    }
}