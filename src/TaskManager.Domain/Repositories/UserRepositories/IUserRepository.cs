using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task<bool> ExistsByEmail(string email);
        Task<User?> GetByEmail(string email);
        Task<User?> GetById(Guid id);
    }
}
