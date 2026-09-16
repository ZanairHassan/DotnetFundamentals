using RepositoryPattern.Models;

namespace RepositoryPattern.Data
{
    public class InMemoryDataStore
    {
        public List<User> Users { get; }

        public List<Career> Careers { get; }

        public InMemoryDataStore()
        {
            Users = UserSeedData.GetUsers();

            Careers = CareerSeedData.GetCareers();
        }
    }

}
