namespace TaskManager.Domain.Repositories.TokenRepositories
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Entities.User user);
    }
}
