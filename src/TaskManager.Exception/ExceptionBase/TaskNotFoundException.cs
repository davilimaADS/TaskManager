namespace TaskManager.Exception.ExceptionBase
{
    public class TaskNotFoundException : TaskManagerException
    {
        public TaskNotFoundException()
            : base("Task not found.")
        {
        }

        public TaskNotFoundException(Guid id)
            : base($"Task with ID '{id}' was not found.")
        {
        }
    }
}
