using AdvanceLinqOperations.Models;

namespace AdvanceLinqOperations.DataSeed
{
    public static class DepartmentSeed
    {
        public static List<Department> SeedDepartments()
        {
            return new List<Department>
            {
                new Department
                {
                    Id = 1,
                    Name = "Development",
                    Location = "Lahore",
                    Budget = 5000000m,
                    ManagerName = "Ahmed Khan"
                },
                new Department
                {
                    Id = 2,
                    Name = "HR",
                    Location = "Karachi",
                    Budget = 15000000m,
                    ManagerName = "Fatima Malik"
                },
                new Department
                {
                    Id = 3,
                    Name = "Accountant",
                    Location = "Islamabad",
                    Budget = 10000000m,
                    ManagerName = "Ali Raza"
                },
                new Department
                {
                    Id = 4,
                    Name = "Sales",
                    Location = "Sialkot",
                    Budget = 8000000m,
                    ManagerName = "Sara Sheikh"
                },
                new Department
                {
                    Id = 5,
                    Name = "Marketing",
                    Location = "Gujranwala",
                    Budget = 7000000m,
                    ManagerName = "Hassan Qureshi"
                }
            };
        }
    }
}