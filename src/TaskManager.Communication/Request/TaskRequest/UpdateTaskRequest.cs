using TaskManager.Domain.Enums;

namespace TaskManager.Communication.Request.TaskRequest
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskEnum TaskType { get; set; }
    }
}
