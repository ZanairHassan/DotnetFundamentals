using WeeklyAssignment.Models;

namespace WeeklyAssignment.Data;

public static class EmployeeSeedData
{
    public static List<Employee> GetEmployees()
    {
        Random random = new(12345);
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

        List<Employee> employees = [];

        for (int i = 1; i <= 20; i++)
        {
            string firstName = firstNames[random.Next(firstNames.Length)];

            string lastName = lastNames[random.Next(lastNames.Length)];

            employees.Add(new Employee
            {
                Id = i,

                Name = $"{firstName} {lastName}",

                Email = $"{firstName.ToLower()}.{lastName.ToLower()}{i}@example.com",

                Salary = random.Next(60000, 250001),

                JoiningDate = DateTime.Today.AddDays(-random.Next(30, 3650)),

                DesignationId = random.Next(1, 6)
            });
        }

        return employees;
    }
}
