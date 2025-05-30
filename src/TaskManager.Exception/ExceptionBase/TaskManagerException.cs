namespace TaskManager.Exception.ExceptionBase
{
    public abstract class TaskManagerException : SystemException
    {
        public TaskManagerException() { }

        public TaskManagerException(string message) : base(message) { }
    }
}
