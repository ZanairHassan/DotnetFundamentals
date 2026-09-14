using RepositoryPattern.Models;

namespace RepositoryPattern.ViewModels
{
    public class UserListVM
    {
        public IReadOnlyList<User> Users { get; set; } = [];

        public IReadOnlyList<Career> Careers { get; set; } = [];

        public string? SearchTerm { get; set; }

        public List<int> SelectedUserIds { get; set; } = [];

        public int? SelectedCareerId { get; set; }
    }
}
