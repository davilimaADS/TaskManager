using TaskManager.Domain.Enums;

namespace TaskManager.Communication.Request.ProjectRequest
{
    public class UpdateProjectRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
    }
}
