namespace TaskManager.Exception.ExceptionBase
{
    public class ProjectNotFoundException : TaskManagerException
    {
        public ProjectNotFoundException(Guid id)
            : base($"Project with ID {id} was not found.")
        {
        }
    }
}
