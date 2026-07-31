using AdvanceLinqOperations.Models;

namespace AdvanceLinqOperations.DataSeed
{
    public static class EmployeeSeed
    {
        public static List<Employee> SeedEmployees()
        {
            Random random = new Random();

            string[] firstNames =
            {
                "Ali", "Ahmed", "Usman", "Hamza", "Bilal",
                "Ayesha", "Fatima", "Sara", "Hina", "Zain",
                "Hassan", "Talha", "Saad", "Noor", "Iqra",
                "Maham", "Danish", "Anum", "Maryam", "Abdullah"
            };

            string[] lastNames =
            {
                "Khan", "Ahmed", "Malik", "Butt", "Qureshi",
                "Sheikh", "Raza", "Javed", "Mirza", "Chaudhry"
            };

            string[] genders =
            {
                "Male",
                "Female"
            };

            string[] cities =
            {
                "Lahore",
                "Karachi",
                "Islamabad",
                "Sialkot",
                "Gujranwala",
            };

            List<Employee> employees = new List<Employee>();

            for (int i = 1; i <= 20; i++)
            {
                string firstName = firstNames[random.Next(firstNames.Length)];
                string lastName = lastNames[random.Next(lastNames.Length)];

                employees.Add(new Employee
                {
                    Id = i,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = random.Next(21, 50),
                    Gender = genders[random.Next(genders.Length)],
                    DepartmentId = random.Next(1, 6),
                    City = cities[random.Next(cities.Length)],
                    Salary = random.Next(30000, 200001),
                    Bonus = random.Next(5000, 50001),
                    Experience = random.Next(0, 11),
                    JoiningDate = DateTime.Today.AddDays(-random.Next(30, 3650)),
                    PerformanceRating = Math.Round(random.Next(10, 51) / 10.0, 1),
                    IsActive = random.Next(2) == 1,
                    Email = $"{firstName.ToLower()}.{lastName.ToLower()}{i}@company.com",
                    Phone = $"03{random.Next(10, 50)}-{random.Next(1000000, 9999999)}"
                });
            }

            return employees;
        }
    }
}