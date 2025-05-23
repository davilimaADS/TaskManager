using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Project
    {
        // chave primaria
        public Guid Id { get; set; }

        // nome do projeto
        public string Name { get; set; } = string.Empty;

        // descrição do projeto 
        public string Description { get; set; } = string.Empty;

        // data de criação do projeto usando DateTime UtcNow
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // data de atualização do projeto
        public DateTime? UpdatedAt { get; set; }

        // data de exclusão
        public DateTime? DeletedAt { get; set; }

        // status do projeto usando enum 
        public ProjectStatus Status { get; set; } = ProjectStatus.InProgress;

        // chave estrangeira do usuário que criou o projeto 
        public Guid OwnerId { get; set; }

        // relacionamento com o usuário que criou o projeto 
        public User Owner { get; set; } = null!;

        // relacionamento com as tarefas do projeto 
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    }
}
