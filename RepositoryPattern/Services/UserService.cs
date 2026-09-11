using RepositoryPattern.Models;
using RepositoryPattern.Repositories.Interfaces;
using RepositoryPattern.Services.Interfaces;

namespace RepositoryPattern.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> CreateAsync(User user)
        {
            bool emailExists = await EmailExistsAsync(user.Email);

            if (emailExists)
            {
                return null;
            }

            return await _userRepository.AddAsync(user);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            if (user.Id <= 0)
            {
                return null;
            }

            User? existingUser = await _userRepository.GetByIdAsync(user.Id);

            if (existingUser is null)
            {
                return null;
            }

            bool emailExists = await EmailExistsForAnotherUserAsync(user.Email, user.Id);

            if (emailExists)
            {
                return null;
            }

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return await _userRepository.DeleteAsync(id);
        }

        private async Task<bool> EmailExistsAsync(string email)
        {
            IReadOnlyList<User> users = await _userRepository.GetAllAsync();

            return users.Any(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<bool> EmailExistsForAnotherUserAsync(string email, int userId)
        {
            IReadOnlyList<User> users = await _userRepository.GetAllAsync();

            return users.Any(user => user.Id != userId && user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
