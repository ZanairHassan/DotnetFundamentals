using RepositoryPattern.Models;

namespace RepositoryPattern.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User> AddAsync(User user);

        Task<User?> UpdateAsync(User user);

        Task<bool> DeleteAsync(int id);
    }
}
