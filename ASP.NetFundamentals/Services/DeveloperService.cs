using ASP.NetFundamentals.Interfaces;
using ASP.NetFundamentals.Models;

namespace ASP.NetFundamentals.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly List<Developer> _developers = new()
        {
            new Developer
            {
                ID = 1,
                Name = "Ali",
                Email = "ali@example.com",
                Field = "Backend",
                Age = 24,
                Role = "Junior Developer",
                Experience = 1.5m,
                Salary = 80000m
            },
            new Developer
            {
                ID = 2,
                Name = "Ahmed",
                Email = "ahmed@example.com",
                Field = "Frontend",
                Age = 27,
                Role = "Senior Developer",
                Experience = 4m,
                Salary = 150000m
            }
        };

        public IEnumerable<Developer> GetAll()
        {
            return _developers;
        }

        public Developer? GetById(int id)
        {
            return _developers.FirstOrDefault(x => x.ID == id);
        }

        public void Create(Developer developer)
        {
            developer.ID = _developers.Count == 0 ? 1 : _developers.Max(x => x.ID) + 1;

            _developers.Add(developer);
        }

        public bool Update(int id, Developer developer)
        {
            var existingDeveloper = GetById(id);

            if (existingDeveloper is null)
            {
                return false;
            }

            existingDeveloper.Name = developer.Name;
            existingDeveloper.Email = developer.Email;
            existingDeveloper.Field = developer.Field;
            existingDeveloper.Age = developer.Age;
            existingDeveloper.Role = developer.Role;
            existingDeveloper.Experience = developer.Experience;
            existingDeveloper.Salary = developer.Salary;

            return true;
        }

        public bool Delete(int id)
        {
            var developer = GetById(id);

            if (developer is null)
            {
                return false;
            }

            _developers.Remove(developer);

            return true;
        }
    }
}