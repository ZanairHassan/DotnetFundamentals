using ASP.NetFundamentals.Models;

namespace ASP.NetFundamentals.Services
{
    public sealed class StudentStore
    {
        private readonly List<Student> _students = [];

        public StudentStore()
        {
            SeedStudents();
        }

        private void SeedStudents()
        {
            _students.AddRange(
            [
                new Student
            {
                ID = 1,
                Name = "Ali Khan",
                Address = "Lahore",
                Age = 22,
                Email = "ali@example.com"
            },
            new Student
            {
                ID = 2,
                Name = "Ahmed Raza",
                Address = "Karachi",
                Age = 23,
                Email = "ahmed@example.com"
            },
            new Student
            {
                ID = 3,
                Name = "Sara Ahmed",
                Address = "Islamabad",
                Age = 21,
                Email = "sara@example.com"
            },
            new Student
            {
                ID = 4,
                Name = "Fatima Noor",
                Address = "Multan",
                Age = 24,
                Email = "fatima@example.com"
            },
            new Student
            {
                ID = 5,
                Name = "Usman Ali",
                Address = "Rawalpindi",
                Age = 22,
                Email = "usman@example.com"
            }
            ]);
        }

        public IReadOnlyList<Student> GetStudents()
        {
            return _students;
        }
    }
}
