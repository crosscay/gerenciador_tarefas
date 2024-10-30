using TaskManager.Api.DTOs;

namespace TaskManager.Api.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetAllPeopleAsync();
        Task<PersonDTO> GetPersonByIdAsync(Guid id);
        Task<Guid> AddPersonAsync(CreatePersonDTO personDto);
        Task UpdatePersonAsync(Guid id, CreatePersonDTO personDto);
        Task<bool> DeletePersonAsync(Guid id);
        Task UpdatePersonStatusAsync(Guid id);
    }
}