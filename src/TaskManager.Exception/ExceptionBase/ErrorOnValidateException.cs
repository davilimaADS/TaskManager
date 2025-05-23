namespace TaskManager.Exception.ExceptionBase
{
    public class ErrorOnValidateException : TaskManagerException 
    {

        public List<string> Errors { get; set; }
        public ErrorOnValidateException( List<string> errorMessages)
        {
            Errors = errorMessages;
        }
    }
}
