using TaskManager.Domain.Enums;

namespace TaskManager.Communication.Response.ProjectResponse
{
    public class UpdateProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
