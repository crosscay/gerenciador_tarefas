namespace TaskManager.Api.Models
{
    public class TaskEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public Enums.TaskStatus Status { get; set; }
        
        public Guid PersonId { get; set; }
        public bool IsCompleted { get; set; }

        public Person Person { get; set; }

        public Guid? AssignedPersonId { get; set; }


        // Constructor
        public TaskEntity()
        {
            Id = Guid.NewGuid(); // Gera um novo UUID para novas instâncias.
            CreatedAt = DateTime.UtcNow; // Gera um novo UUID para novas instâncias.
        }
    }
}