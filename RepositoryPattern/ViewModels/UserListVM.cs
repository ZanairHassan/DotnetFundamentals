using RepositoryPattern.Models;

namespace RepositoryPattern.ViewModels
{
    public class UserListVM
    {
        public IReadOnlyList<User> Users { get; set; } = [];

        public string? SearchTerm { get; set; }
    }
}
