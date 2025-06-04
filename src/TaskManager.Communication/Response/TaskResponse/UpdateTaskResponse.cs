using TaskManager.Domain.Enums;

namespace TaskManager.Communication.Response.TaskResponse
{
    public class UpdateTaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskEnum TaskType { get; set; } 
        public DateTime UpdatedAt { get; set; }
    }
}
