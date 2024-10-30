using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Api.DTOs;
using TaskManager.Api.Services.Interfaces;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        /// <summary>
        /// Obtém todas as pessoas cadastradas.
        /// </summary>
        /// <returns>Uma lista de pessoas.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtém todas as pessoas cadastradas")]
        [SwaggerResponse(200, "Retorna uma lista de pessoas.")]
        public async Task<IActionResult> Get()
        {
            var people = await _personService.GetAllPeopleAsync();
            return Ok(people);
        }

        /// <summary>
        /// Obtém uma pessoa pelo ID.
        /// </summary>
        /// <param name="id">ID da pessoa.</param>
        /// <returns>A pessoa correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém uma pessoa pelo ID")]
        [SwaggerResponse(200, "Retorna a pessoa correspondente ao ID.")]
        [SwaggerResponse(404, "Pessoa não encontrada.")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var person = await _personService.GetPersonByIdAsync(id);
            return person != null ? Ok(person) : NotFound();
        }

        /// <summary>
        /// Cadastra uma nova pessoa.
        /// </summary>
        /// <param name="personDto">Os dados da nova pessoa.</param>
        /// <returns>O ID da nova pessoa criada.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra uma nova pessoa")]
        [SwaggerResponse(201, "Pessoa criada com sucesso.")]
        public async Task<IActionResult> Post([FromBody] CreatePersonDTO personDto)
        {
            // Chama o serviço e obtém o novo ID gerado
            var newPersonId = await _personService.AddPersonAsync(personDto);
            
            // Retorna um CreatedAtAction com o novo ID
            return CreatedAtAction(nameof(GetById), new { id = newPersonId }, personDto);
        }


        /// <summary>
        /// Atualiza os dados de uma pessoa existente.
        /// </summary>
        /// <param name="id">ID da pessoa a ser atualizada.</param>
        /// <param name="personDto">Os novos dados da pessoa.</param>
        /// <returns>Status da operação.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza os dados de uma pessoa existente")]
        [SwaggerResponse(204, "Pessoa atualizada com sucesso.")]
        [SwaggerResponse(400, "Dados inválidos.")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PersonDTO personDto)
        {
            try
            {
                await _personService.UpdatePersonAsync(id, personDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma pessoa pelo ID.
        /// </summary>
        /// <param name="id">ID da pessoa a ser removida.</param>
        /// <returns>Status da operação.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove uma pessoa pelo ID")]
        [SwaggerResponse(204, "Pessoa removida com sucesso.")]
        [SwaggerResponse(404, "Pessoa não encontrada.")]
        [SwaggerResponse(400, "Não é possível excluir a pessoa.")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _personService.DeletePersonAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (CannotDeletePersonException ex)
            {
                // Devolve um código 400 (Bad Request) com uma mensagem de erro
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Tratamento genérico de outras exceções
                return StatusCode(500, new { message = "Ocorreu um erro inesperado.", details = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza o status de uma pessoa.
        /// </summary>
        /// <param name="id">ID da pessoa.</param>
        /// <returns>Status da operação.</returns>
        [HttpPut("{id}/status")]
        [SwaggerOperation(Summary = "Atualiza o status de uma pessoa")]
        [SwaggerResponse(204, "Status da pessoa atualizado com sucesso.")]
        [SwaggerResponse(400, "Dados inválidos.")]
        public async Task<IActionResult> UpdateStatus(Guid id)
        {
            try
            {
                await _personService.UpdatePersonStatusAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}