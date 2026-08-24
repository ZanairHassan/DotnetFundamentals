using RepositoryPattern.Models;

namespace RepositoryPattern.Services.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User?> CreateAsync(User user);

        Task<User?> UpdateAsync(User user);

        Task<bool> DeleteAsync(int id);
    }
}
