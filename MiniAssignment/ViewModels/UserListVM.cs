using MiniAssignment.Models;

namespace MiniAssignment.ViewModels
{
    public class UserListVM
    {
        public IEnumerable<User> Users { get; set; } = [];

        public int TotalUsers { get; set; }
    }
}