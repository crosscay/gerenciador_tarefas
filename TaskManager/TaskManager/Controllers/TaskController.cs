using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTOs;
using TaskManager.Api.Services.Interfaces;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/task/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTaskById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/task
        [HttpPost]
        public async Task<ActionResult> AddTask(CreateTaskDTO taskDto)
        {
            var newTaskId = await _taskService.AddTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = newTaskId }, taskDto);
        }

        // PUT: api/task/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(Guid id, TaskDTO taskDto)
        {
            await _taskService.UpdateTaskAsync(id, taskDto);
            return NoContent();
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/task/assign/{taskId}/{personId}
        [HttpPost("assign/{taskId}/{personId}")]
        public async Task<ActionResult> AssignTaskToPerson(Guid taskId, Guid personId)
        {
            var result = await _taskService.AssignTaskToPersonAsync(taskId, personId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{taskId}/unassign")]
        public async Task<IActionResult> UnassignTask(Guid taskId)
        {
            bool result = await _taskService.UnassignTaskAsync(taskId);
            
            if (!result)
            {
                return NotFound($"Não foi encontrada a tarefa com ID: {taskId} ou já está sem atribuição.");
            }
            
            return Ok($"A atribuição da tarefa com ID: {taskId} foi removida.");
        }
    }
}