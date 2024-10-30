using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _personService.GetAllPeopleAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var person = await _personService.GetPersonByIdAsync(id);
            return person != null ? Ok(person) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePersonDTO personDto)
        {
            // Chama o serviço e obtém o novo ID gerado
            var newPersonId = await _personService.AddPersonAsync(personDto);
            
            // Retorna um CreatedAtAction com o novo ID
            return CreatedAtAction(nameof(GetById), new { id = newPersonId }, personDto);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpPut("{id}/status")]
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