using AutoMapper;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Api.Services.Interfaces;

namespace TaskManager.Api.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository; // Para asignar tareas a personas
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IPersonRepository personRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public async Task<TaskDTO> GetTaskByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<Guid> AddTaskAsync(CreateTaskDTO taskDto)
        {
            var taskEntity = _mapper.Map<TaskEntity>(taskDto);

            // Gere um novo UUID para a entidade Pessoa.
            taskEntity.Id = Guid.NewGuid();

            await _taskRepository.AddAsync(taskEntity);

            // Retorne o ID gerado
            return taskEntity.Id;
        }

        public async Task UpdateTaskAsync(Guid id, TaskDTO taskDto)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            if (taskEntity == null) return;

            _mapper.Map(taskDto, taskEntity); // Atualizar os valores existentes.
            await _taskRepository.UpdateAsync(taskEntity);
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

        public async Task<bool> AssignTaskToPersonAsync(Guid taskId, Guid personId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            var person = await _personRepository.GetByIdAsync(personId);

            if (task == null || person == null) return false;

            task.AssignedPersonId = personId; // Assumindo que você tem uma propriedade AssignedPersonId em TaskEntity.
            await _taskRepository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> UnassignTaskAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) return false;

            task.AssignedPersonId = null; // Remover atribuição
            await _taskRepository.UpdateAsync(task);
            return true;
        }

    }
}