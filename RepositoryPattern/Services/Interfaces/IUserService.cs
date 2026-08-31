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

        Task<bool> AssignCareerAsync(IReadOnlyCollection<int> userIds, int careerId);

        Task<IReadOnlyList<Career>> GetCareersAsync();
    }
}
