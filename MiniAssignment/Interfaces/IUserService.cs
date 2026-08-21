using MiniAssignment.Models;

namespace MiniAssignment.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User? GetById(int id);

        void Add(User user);

        void Update(User user);

        void Delete(int id);

        IEnumerable<User> GetActiveUsers();

        IEnumerable<User> GetInactiveUsers();
    }
}
