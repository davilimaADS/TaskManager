namespace TaskManager.Domain.HttpContext
{
    public interface IUserContext
    {
        Guid GetCurrentUserId();
    }
}
