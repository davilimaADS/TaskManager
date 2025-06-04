namespace TaskManager.Communication.Response.TaskResponse
{
    public class GetAllTaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TaskType { get; set; } = string.Empty;  
        public DateTime CreatedAt { get; set; }
        public Guid ProjectId { get; set; }
    }
}
