namespace TaskManager.Exception.ExceptionBase
{
    public class UserNotFoundException : TaskManagerException
    {
        public UserNotFoundException(Guid id)
            : base($"User with ID {id} was not found.")
        {
        }
    }
}
