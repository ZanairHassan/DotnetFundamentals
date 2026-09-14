using RepositoryPattern.Data;
using RepositoryPattern.Models;
using RepositoryPattern.Repositories.Interfaces;
using System.Xml.Linq;

namespace RepositoryPattern.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepository(InMemoryDataStore dataStore)
        {
            _users = dataStore.Users;
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            IReadOnlyList<User> users = _users.AsReadOnly();

            return Task.FromResult(users);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            User? user = _users.FirstOrDefault(user => user.Id == id);

            return Task.FromResult(user);
        }

        public Task<User> CreateAsync(User user)
        {
            int nextId = _users.Count == 0 ? 1 : _users.Max(user => user.Id) + 1;

            user.Id = nextId;

            _users.Add(user);

            return Task.FromResult(user);
        }

        public Task<User?> UpdateAsync(User user)
        {
            User? existingUser = _users.FirstOrDefault(existingUser => existingUser.Id == user.Id);

            if (existingUser is null)
            {
                return Task.FromResult<User?>(null);
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.IsActive = user.IsActive;

            return Task.FromResult<User?>(existingUser);
        }

        public Task<bool> DeleteAsync(int id)
        {
            User? user = _users.FirstOrDefault(user => user.Id == id);

            if (user is null)
            {
                return Task.FromResult(false);
            }

            _users.Remove(user);

            return Task.FromResult(true);
        }
    }
}
