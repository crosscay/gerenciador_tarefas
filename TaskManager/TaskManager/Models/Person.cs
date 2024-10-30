namespace TaskManager.Api.Models
{
    public class Person
    {
        public Guid Id { get; set; }    

        public string Name { get; set; } // Nombre

        public string Email { get; set; } // Email

        public DateTime DateOfBirth { get; set; } // Data de nascimento

        public ICollection<TaskEntity> Tasks { get; set; } // Tarefas associadas

        // Nuevo estado de disponibilidad
        public Enums.PersonStatus Status { get; set; } 

        // Construtor
        public Person()
        {
            Id = Guid.NewGuid(); // Gera um novo UUID para novas instancias
            Tasks = new List<TaskEntity>();
        }
    }
}