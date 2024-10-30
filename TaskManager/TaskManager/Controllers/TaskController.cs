using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Api.DTOs;
using TaskManager.Api.Services.Interfaces;

namespace TaskManager.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar as tarefas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="TaskController"/>.
        /// </summary>
        /// <param name="taskService">O serviço de tarefas utilizado para gerir as operações.</param>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        /// <returns>Uma lista de <see cref="TaskDTO"/> que representa as tarefas.</returns>
        // GET: api/task
        [HttpGet]
        [SwaggerOperation(Summary = "Obtém todas as tarefas.")]
        [SwaggerResponse(200, "Lista de tarefas obtida com sucesso.", typeof(IEnumerable<TaskDTO>))]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Obtém uma tarefa pelo ID.
        /// </summary>
        /// <param name="id">O ID da tarefa.</param>
        /// <returns>A tarefa correspondente ao ID fornecido.</returns>
        // GET: api/task/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém uma tarefa pelo ID.")]
        [SwaggerResponse(200, "Tarefa obtida com sucesso.", typeof(TaskDTO))]
        [SwaggerResponse(404, "Tarefa não encontrada.")]
        public async Task<ActionResult<TaskDTO>> GetTaskById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        /// <summary>
        /// Adiciona uma nova tarefa.
        /// </summary>
        /// <param name="taskDto">O objeto que contém os dados da tarefa a ser criada.</param>
        /// <returns>O ID da nova tarefa criada.</returns>
        // POST: api/task
        [HttpPost]
        [SwaggerOperation(Summary = "Adiciona uma nova tarefa.")]
        [SwaggerResponse(201, "Tarefa criada com sucesso.", typeof(Guid))]
        [SwaggerResponse(400, "Dados inválidos.")]
        public async Task<ActionResult> AddTask(CreateTaskDTO taskDto)
        {
            var newTaskId = await _taskService.AddTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = newTaskId }, taskDto);
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="id">O ID da tarefa a ser atualizada.</param>
        /// <param name="taskDto">O objeto que contém os novos dados da tarefa.</param>
        /// <returns>Uma resposta vazia se a atualização for bem-sucedida.</returns>
        // PUT: api/task/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma tarefa existente.")]
        [SwaggerResponse(204, "Tarefa atualizada com sucesso.")]
        [SwaggerResponse(400, "Dados inválidos.")]
        [SwaggerResponse(404, "Tarefa não encontrada.")]
        public async Task<ActionResult> UpdateTask(Guid id, CreateTaskDTO taskDto)
        {
            await _taskService.UpdateTaskAsync(id, taskDto);
            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa pelo ID.
        /// </summary>
        /// <param name="id">O ID da tarefa a ser excluída.</param>
        /// <returns>Uma resposta vazia se a exclusão for bem-sucedida.</returns>
        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui uma tarefa pelo ID.")]
        [SwaggerResponse(204, "Tarefa excluída com sucesso.")]
        [SwaggerResponse(404, "Tarefa não encontrada.")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Atribui uma tarefa a uma pessoa.
        /// </summary>
        /// <param name="taskId">O ID da tarefa que será atribuída.</param>
        /// <param name="personId">O ID da pessoa a quem a tarefa será atribuída.</param>
        /// <returns>Uma resposta vazia se a atribuição for bem-sucedida.</returns>
        // POST: api/task/assign/{taskId}/{personId}
        [HttpPost("assign/{taskId}/{personId}")]
        [SwaggerOperation(Summary = "Atribui uma tarefa a uma pessoa.")]
        [SwaggerResponse(204, "Tarefa atribuída com sucesso.")]
        [SwaggerResponse(404, "Tarefa ou pessoa não encontrada.")]
        public async Task<ActionResult> AssignTaskToPerson(Guid taskId, Guid personId)
        {
            var result = await _taskService.AssignTaskToPersonAsync(taskId, personId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Desatribui uma tarefa.
        /// </summary>
        /// <param name="taskId">O ID da tarefa a ser desatribuída.</param>
        /// <returns>Uma mensagem que indica o resultado da operação.</returns>
        // PUT: api/task/{taskId}/unassign
        [HttpPut("{taskId}/unassign")]
        [SwaggerOperation(Summary = "Desatribui uma tarefa.")]
        [SwaggerResponse(200, "Atribuição da tarefa removida com sucesso.")]
        [SwaggerResponse(404, "Tarefa não encontrada ou já desatribuída.")]
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