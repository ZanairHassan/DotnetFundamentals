using MiniAssignment.DataSeed;
using MiniAssignment.Interfaces;
using MiniAssignment.Models;

namespace MiniAssignment.Services
{
    public class UserService : IUserService
    {
        private int _nextId;
        private readonly List<User> _users;

        public UserService()
        {
            _users = UserSeed.SeedUsers();
            _nextId = _users.Count == 0 ? 1 : _users.Max(user => user.ID) + 1;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User? GetById(int id)
        {
            return _users.FirstOrDefault(user => user.ID == id);
        }

        public void Add(User user)
        {
            if (_users.Any(existingUser => existingUser.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("A user with this email address already exists.");
            }

            user.ID = _nextId++;

            _users.Add(user);
        }

        public void Update(User user)
        {
            var existingUser = _users.FirstOrDefault(existing => existing.ID == user.ID);

            if (existingUser is null)
            {
                throw new InvalidOperationException($"User with ID {user.ID} was not found.");
            }

            bool duplicateEmail = _users.Any(existing => existing.ID != user.ID && existing.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase));

            if (duplicateEmail)
            {
                throw new InvalidOperationException("A user with this email address already exists.");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.IsActive = user.IsActive;
        }

        public void Delete(int id)
        {
            var user = _users.FirstOrDefault(existingUser => existingUser.ID == id);

            if (user is null)
            {
                throw new InvalidOperationException($"User with ID {id} was not found.");
            }

            _users.Remove(user);
        }

        public IEnumerable<User> GetActiveUsers()
        {
            return _users.Where(user => user.IsActive);
        }

        public IEnumerable<User> GetInactiveUsers()
        {
            return _users.Where(user => !user.IsActive);
        }
    }
}
