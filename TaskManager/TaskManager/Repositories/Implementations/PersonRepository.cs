using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Models;

namespace TaskManager.Api.Repositories.Implementations
{
    public class PersonRepository : Interfaces.IPersonRepository
    {
        private readonly TaskManagerDbContext _context;

        public PersonRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task AddAsync(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var person = await GetByIdAsync(id);
            if (person == null)
                return false;

            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        // public async Task<bool> HasPendingTasksAsync(Guid personId)
        // {
        //     // Assumindo que você tem uma relação de tarefas no seu contexto
        //     return await _context.Tasks.AnyAsync(t => t.PersonId == personId && t.Status == "pendente");
        // }

        public async Task<bool> HasPendingTasksAsync(Guid personId)
        {
            // Consulta las tareas asociadas a la persona y verifica si hay alguna que no esté completada
            return await _context.Tasks
                .Where(t => t.PersonId == personId && !t.IsCompleted) // Asegúrate de tener un campo 'IsCompleted' en tus tareas
                .AnyAsync();
        }
    }
}