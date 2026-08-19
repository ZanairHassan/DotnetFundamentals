using ASP.NetFundamentals.Interfaces;
using ASP.NetFundamentals.Models;

namespace ASP.NetFundamentals.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly List<Employee> _employees = new();

        public EmployeeService()
        {
            _employees.Add(new Employee
            {
                ID = 1,
                Name = "Ali Khan",
                Email = "ali@example.com",
                Age = 28,
                Salary = 75000,
                Department = "IT",
                Designation = "Software Engineer",
                IsAvailable = true
            });

            _employees.Add(new Employee
            {
                ID = 2,
                Name = "Ahmed Raza",
                Email = "ahmed@example.com",
                Age = 32,
                Salary = 95000,
                Department = "HR",
                Designation = "HR Manager",
                IsAvailable = true
            });
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public Employee? GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _employees;
            }

            searchTerm = searchTerm.Trim();

            if (int.TryParse(searchTerm, out int employeeId))
            {
                return _employees
                    .Where(e => e.ID == employeeId)
                    .ToList();
            }

            return _employees
                .Where(e => e.Name.Contains(
                    searchTerm,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Add(Employee employee)
        {
            int nextId = _employees.Count == 0 ? 1 : _employees.Max(e => e.ID) + 1;

            employee.ID = nextId;

            _employees.Add(employee);
        }

        public bool Update(Employee employee)
        {
            Employee? existingEmployee = GetById(employee.ID);

            if (existingEmployee is null)
            {
                return false;
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.Age = employee.Age;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.Department = employee.Department;
            existingEmployee.Designation = employee.Designation;
            existingEmployee.IsAvailable = employee.IsAvailable;

            return true;
        }

        public bool Delete(int id)
        {
            Employee? employee = GetById(id);

            if (employee is null)
            {
                return false;
            }

            _employees.Remove(employee);

            return true;
        }
    }
}

