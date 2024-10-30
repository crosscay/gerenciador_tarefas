using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs
{
    public class CreateTaskDTO
    {
        [Required(ErrorMessage = "O título da tarefa é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título não pode ter mais de 200 caracteres.")]
        public string Title { get; set; }           // Título da tarefa


        [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres.")]
        public string Description { get; set; }     // Descrição da tarefa.


        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime CreatedAt { get; set; }     // Data de criacao


        [Required(ErrorMessage = "O estado da tarefa é obrigatório")]
        public Enums.TaskStatus Status { get; set; }    // Estado da tarefa (pendente, em progresso, concluída).

        
        [Required(ErrorMessage = "O ID da pessoa responsável é obrigatório.")]
        public Guid PersonId { get; set; }              // ID da pessoa responsável

        public Guid? AssignedPersonId { get; set; }     // ID da pessoa à qual a tarefa foi atribuída (opcional)
    }

    
    public class TaskDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } // Data de criacao
        public Enums.TaskStatus Status { get; set; }
        public Guid PersonId { get; set; }
        public Guid? AssignedPersonId { get; set; }     // ID da pessoa à qual a tarefa foi atribuída (opcional)
    }
}