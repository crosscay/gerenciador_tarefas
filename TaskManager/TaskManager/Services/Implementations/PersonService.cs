using AutoMapper;
using TaskManager.Api.DTOs;
using TaskManager.Api.Enums;
using TaskManager.Api.Models;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Api.Services.Interfaces;

namespace TaskManager.Api.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllPeopleAsync()
        {
            var people = await _personRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonDTO>>(people);
        }

        public async Task<PersonDTO> GetPersonByIdAsync(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<Guid> AddPersonAsync(CreatePersonDTO personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            
            // Genera un nuevo UUID para la entidad Person
            person.Id = Guid.NewGuid();

            await _personRepository.AddAsync(person);

            // Devuelve el Id generado
            return person.Id;
        }

        public async Task UpdatePersonAsync(Guid id, PersonDTO personDto)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) throw new Exception("Pessoa não encontrada.");

            _mapper.Map(personDto, person);
            await _personRepository.UpdateAsync(person);
        }


        public async Task<bool> DeletePersonAsync(Guid id)
        {
            if (await _personRepository.HasPendingTasksAsync(id))
            {
                throw new CannotDeletePersonException("Não é possível deletar a pessoa com tarefas pendentes.");
            }
            
            var person = await _personRepository.GetByIdAsync(id);
            await _personRepository.DeleteAsync(id);
            
            // Verificar si todas las tareas están completadas después de la eliminación
            if (!await _personRepository.HasPendingTasksAsync(id))
            {
                person.Status = PersonStatus.Available;
                await _personRepository.UpdateAsync(person); // Asegúrate de que tienes un método para actualizar el estado
            }
            
            return true;
        }


        public async Task UpdatePersonStatusAsync(Guid id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            
            if (person == null)
                throw new Exception("Pessoa não encontrada.");
            
            // Verifica si todas las tareas están completadas
            var allTasksCompleted = !await _personRepository.HasPendingTasksAsync(id);
            person.Status = allTasksCompleted ? PersonStatus.Available : PersonStatus.Unavailable;

            await _personRepository.UpdateAsync(person);
        }
    }
}