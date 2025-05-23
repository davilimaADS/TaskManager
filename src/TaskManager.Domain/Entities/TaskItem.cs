using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        // chave primaria da entidade 
        public Guid Id { get; set; }
        
        // titulo da tarefa
        public string Title { get; set; } = string.Empty;
        
        // descricao da tarefa
        public string Description { get; set; } = string.Empty;
        
        // tipo da tarefa usando enumn
        public TaskEnum TaskType { get; set; } = TaskEnum.Pending;
        
        // data de criacao da tarefa usando datetime utc now
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // data de atualizacao da tarefa 
        public DateTime? UpdatedAt { get; set; }
        
        // data de exclusao da tarefa
        public DateTime? DeletedAt { get; set; }

        // chave estrangeira para o projeto que a tarefa pertence 
        public Guid ProjectId { get; set; }
        
        // relacionamento com o projeto que a tarefa pertence 
        public Project Project { get; set; } = null!;

        // chave estrangeira para o usuario que a tarefa foi atribuida  
        public Guid AssignedUserId { get; set; }
         
        // relacionamento com o usuario que a tarefa foi atribuida  
        public User AssignedUser { get; set; } = null!;
    }
}
