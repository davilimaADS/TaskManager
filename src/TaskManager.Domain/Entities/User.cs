namespace TaskManager.Domain.Entities
{
    public class User
    {
        // chave primaria

        public Guid Id { get; set; }
        // propriedade nome

        public string Name { get; set; } = string.Empty;
        // propriedade email
        public string Email { get; set; } = string.Empty;

        // Senha criptografada com BCrypt 
        public string PasswordHash { get; set; } = string.Empty;

        // Data de criação do usuário usando o padrão UtcNow que retorna a data e hora atual no fuso horário UTC (Universal Coordinated Time)
        // padrão internacional de referência para a data e hora.                
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamento de 1 para muitos com a entidade Project 
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        // Relacionamento de 1 para muitos com a entidade Task
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    }
}
