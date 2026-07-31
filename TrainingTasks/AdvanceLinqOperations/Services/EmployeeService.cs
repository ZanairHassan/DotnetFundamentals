using AdvanceLinqOperations.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdvanceLinqOperations.Services
{
    public class EmployeeService
    {
        private readonly List<Employee> _employees;
        private readonly List<Department> _departments;
        public EmployeeService(List<Employee> employees, List<Department> departments)
        {
            _employees = employees;
            _departments = departments;
        }
        #region Filtering

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public IEnumerable<Employee> GetActiveEmployees()
        {
            return _employees.Where(e => e.IsActive);
        }

        public IEnumerable<Employee> GetInactiveEmployees()
        {
            return _employees.Where(e => !e.IsActive);
        }

        public IEnumerable<Employee> GetEmployeesByCity(string city)
        {
            return _employees.Where(e =>
                e.City.Equals(city, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Employee> GetEmployeesByDepartment(int departmentId)
        {
            return _employees.Where(e => e.DepartmentId == departmentId);
        }

        public IEnumerable<Employee> GetEmployeesAboveSalary(decimal salary)
        {
            return _employees.Where(e => e.Salary > salary);
        }

        public IEnumerable<Employee> GetEmployeesBelowSalary(decimal salary)
        {
            return _employees.Where(e => e.Salary < salary);
        }

        public IEnumerable<Employee> GetEmployeesByExperience(int minimumExperience)
        {
            return _employees.Where(e => e.Experience >= minimumExperience);
        }

        public IEnumerable<Employee> GetEmployeesByAgeRange(int minimumAge, int maximumAge)
        {
            return _employees.Where(e =>
                e.Age >= minimumAge &&
                e.Age <= maximumAge);
        }

        public IEnumerable<Employee> GetEmployeesJoinedAfter(DateTime joiningDate)
        {
            return _employees.Where(e => e.JoiningDate > joiningDate);
        }

        public IEnumerable<Employee> GetTopPerformers(double minimumRating)
        {
            return _employees.Where(e => e.PerformanceRating >= minimumRating);
        }

        public IEnumerable<Employee> SearchEmployees(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return Enumerable.Empty<Employee>();
            }

            keyword = keyword.Trim();

            return _employees.Where(e =>
                e.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Projection

        public IEnumerable<object> GetEmployeeBasicInformation()
        {
            return _employees.Select(employee => new
            {
                employee.Id,
                employee.FullName,
                employee.DepartmentId,
                employee.Salary
            });
        }

        public IEnumerable<string> GetAllPhoneNumbers()
        {
            //issy practically implement krne ky lia phone vali property ko collection declare krna pry ga
            return _employees.SelectMany(employee =>
                new List<string>
                {
            employee.Phone
                });
        }

        #endregion

        #region Sorting

        public IEnumerable<Employee> GetEmployeesOrderedBySalary()
        {
            return _employees.OrderBy(employee => employee.Salary);
        }

        public IEnumerable<Employee> GetEmployeesOrderedByPerformance()
        {
            return _employees.OrderByDescending(employee => employee.PerformanceRating);
        }

        public IEnumerable<Employee> GetEmployeesOrderedByDepartmentThenName()
        {
            return _employees
                .OrderBy(employee => employee.DepartmentId)
                .ThenBy(employee => employee.FirstName);
        }

        public IEnumerable<Employee> GetEmployeesOrderedByDepartmentThenSalary()
        {
            return _employees
                .OrderBy(employee => employee.DepartmentId)
                .ThenByDescending(employee => employee.Salary);
        }

        public IEnumerable<Employee> GetEmployeesInReverseOrder()
        {
            // hum reverse ko directly use nhi kr skty linq me 
            return _employees.AsEnumerable().Reverse();
        }

        #endregion

        #region Element Operators

        public Employee GetFirstActiveEmployee()
        {
            return _employees.First(employee => employee.IsActive);
        }

        public Employee? GetEmployeeByEmail(string email)
        {
            return _employees.FirstOrDefault(employee =>
                employee.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public Employee GetLastJoinedEmployee()
        {
            return _employees
                .OrderBy(employee => employee.JoiningDate)
                .Last();
        }

        public Employee? GetLastEmployeeByCity(string city)
        {
            return _employees
                .Where(employee =>
                    employee.City.Equals(city, StringComparison.OrdinalIgnoreCase))
                .LastOrDefault();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employees.Single(employee => employee.Id == id);
        }

        public Employee? GetEmployeeByUniqueEmail(string email)
        {
            return _employees.SingleOrDefault(employee =>
                employee.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public Employee GetEmployeeAtIndex(int index)
        {
            return _employees.ElementAt(index);
        }

        public Employee? GetEmployeeAtIndexOrDefault(int index)
        {
            return _employees.ElementAtOrDefault(index);
        }

        #endregion

        #region Quantifiers

        public bool HasInactiveEmployees()
        {
            return _employees.Any(employee => !employee.IsActive);
        }

        public bool AreAllEmployeesAdults()
        {
            return _employees.All(employee => employee.Age >= 18);
        }

        public bool EmployeeExists(int employeeId)
        {
            return _employees
                .Select(employee => employee.Id)
                .Contains(employeeId);
        }

        #endregion

        #region Aggregation

        public int GetEmployeeCount()
        {
            return _employees.Count();
        }

        public long GetActiveEmployeeCount()
        {
            return _employees.LongCount(employee => employee.IsActive);
        }

        public decimal GetTotalPayroll()
        {
            return _employees.Sum(employee => employee.Salary + employee.Bonus);
        }

        public decimal GetAverageSalary()
        {
            return _employees.Average(employee => employee.Salary);
        }

        public decimal GetMinimumSalary()
        {
            return _employees.Min(employee => employee.Salary);
        }

        public double GetHighestPerformanceRating()
        {
            return _employees.Max(employee => employee.PerformanceRating);
        }

        public string GetEmployeeNames()
        {
            return _employees
                .Select(employee => employee.FullName)
                .Aggregate((current, next) => $"{current}, {next}");
        }

        #endregion

        #region Grouping

        public IEnumerable<IGrouping<int, Employee>> GroupEmployeesByDepartment()
        {
            return _employees.GroupBy(employee => employee.DepartmentId);
        }

        #endregion

        #region Set Operators

        public IEnumerable<string> GetDistinctCities()
        {
            return _employees
                .Select(employee => employee.City)
                .Distinct();
        }

        public IEnumerable<Employee> GetDistinctDepartments()
        {
            return _employees
                .DistinctBy(employee => employee.DepartmentId);
        }

        public IEnumerable<string> GetUniqueEmployeeNames()
        {
            return _employees
                .Select(employee => employee.FirstName)
                .Union(_employees.Select(employee => employee.LastName));
        }

        public IEnumerable<Employee> GetUnionByDepartment()
        {
            var inactiveEmployees = _employees.Where(employee => !employee.IsActive);

            return _employees
                .UnionBy(inactiveEmployees, employee => employee.DepartmentId);
        }

        public IEnumerable<string> GetCommonCities()
        {
            var activeCities = _employees
                .Where(employee => employee.IsActive)
                .Select(employee => employee.City);

            var inactiveCities = _employees
                .Where(employee => !employee.IsActive)
                .Select(employee => employee.City);

            return activeCities.Intersect(inactiveCities);
        }

        public IEnumerable<Employee> GetEmployeesByCommonDepartments()
        {
            var inactiveDepartmentIds = _employees
                .Where(employee => !employee.IsActive)
                .Select(employee => employee.DepartmentId);

            return _employees
                .IntersectBy(inactiveDepartmentIds, employee => employee.DepartmentId);
        }

        public IEnumerable<string> GetCitiesOnlyForActiveEmployees()
        {
            var activeCities = _employees
                .Where(employee => employee.IsActive)
                .Select(employee => employee.City);

            var inactiveCities = _employees
                .Where(employee => !employee.IsActive)
                .Select(employee => employee.City);

            return activeCities.Except(inactiveCities);
        }

        public IEnumerable<Employee> GetEmployeesExceptInactiveDepartments()
        {
            var inactiveDepartmentIds = _employees
                .Where(employee => !employee.IsActive)
                .Select(employee => employee.DepartmentId);

            return _employees
                .ExceptBy(inactiveDepartmentIds, employee => employee.DepartmentId);
        }

        #endregion

        #region Partitioning

        public IEnumerable<Employee> GetFirstFiveEmployees()
        {
            return _employees.Take(5);
        }

        public IEnumerable<Employee> GetEmployeesWhileSalaryBelow80000()
        {
            return _employees
                .OrderBy(employee => employee.Salary)
                .TakeWhile(employee => employee.Salary < 80000);
        }

        public IEnumerable<Employee> SkipFirstFiveEmployees()
        {
            return _employees.Skip(5);
        }

        public IEnumerable<Employee> SkipEmployeesWhileSalaryBelow80000()
        {
            return _employees
                .OrderBy(employee => employee.Salary)
                .SkipWhile(employee => employee.Salary < 80000);
        }

        public IEnumerable<Employee[]> GetEmployeeBatches()
        {
            return _employees.Chunk(5);
        }

        #endregion

        #region Conversion

        public List<Employee> GetActiveEmployeesAsList()
        {
            return _employees
                .Where(employee => employee.IsActive)
                .ToList();
        }

        public Employee[] GetEmployeesAsArray()
        {
            return _employees.ToArray();
        }

        public Dictionary<int, Employee> GetEmployeeDictionary()
        {
            return _employees.ToDictionary(employee => employee.Id);
        }

        public HashSet<string> GetUniqueCities()
        {
            return _employees
                .Select(employee => employee.City)
                .ToHashSet();
        }

        public ILookup<int, Employee> GetEmployeesLookupByDepartment()
        {
            return _employees.ToLookup(employee => employee.DepartmentId);
        }

        public IEnumerable<Employee> GetEmployeesUsingOfType()
        {
            List<object> items =
            [
                _employees[0],
        "Hello World",
        100,
        _employees[1],
        DateTime.Now
            ];

            return items.OfType<Employee>();
        }

        public IEnumerable<Employee> GetEmployeesUsingCast()
        {
            ArrayList employees = new();

            foreach (Employee employee in _employees)
            {
                employees.Add(employee);
            }

            return employees.Cast<Employee>();
        }

        #endregion

        #region Join

        public IEnumerable<object> GetEmployeesWithDepartments()
        {
            return _employees.Join(
                _departments,
                employee => employee.DepartmentId,
                department => department.Id,
                (employee, department) => new
                {
                    employee.Id,
                    employee.FullName,
                    Department = department.Name,
                    employee.Salary
                });
        }

        public IEnumerable<object> GetDepartmentsWithEmployees()
        {
            return _departments.GroupJoin(
                _employees,
                department => department.Id,
                employee => employee.DepartmentId,
                (department, employees) => new
                {
                    Department = department.Name,
                    Employees = employees
                });
        }

        public IEnumerable<object> GetEmployeeDepartmentPairs()
        {
            return _employees
                .Select(employee => employee.FullName)
                .Zip(
                    _departments.Select(department => department.Name),
                    (employeeName, departmentName) => new
                    {
                        Employee = employeeName,
                        Department = departmentName
                    });
        }

        #endregion

        #region Statistics

        public object GetEmployeeStatistics()
        {
            return new
            {
                TotalEmployees = _employees.Count(),
                ActiveEmployees = _employees.Count(e => e.IsActive),
                TotalPayroll = _employees.Sum(e => e.Salary),
                AverageSalary = _employees.Average(e => e.Salary),
                HighestSalary = _employees.Max(e => e.Salary),
                LowestSalary = _employees.Min(e => e.Salary)
            };
        }

        #endregion

        #region Paging

        public IEnumerable<Employee> GetEmployeesByPage(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");
            }

            return _employees
                .OrderBy(employee => employee.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public int GetTotalPages(int pageSize)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            return (int)Math.Ceiling(_employees.Count() / (double)pageSize);
        }

        #endregion

        #region Complex Queries

        public IEnumerable<Employee> GetTopFiveHighestPaidActiveEmployees()
        {
            return _employees
                .Where(employee => employee.IsActive)
                .OrderByDescending(employee => employee.Salary)
                .Take(5);
        }

        public IEnumerable<object> GetDepartmentEmployeeStatistics()
        {
            return _employees
                .GroupBy(employee => employee.DepartmentId)
                .Select(group => new
                {
                    DepartmentId = group.Key,
                    EmployeeCount = group.Count()
                });
        }

        public IEnumerable<Employee> GetEmployeesAboveAverageSalary()
        {
            decimal averageSalary = _employees.Average(employee => employee.Salary);

            return _employees
                .Where(employee => employee.Salary > averageSalary)
                .OrderByDescending(employee => employee.Salary);
        }

        public IEnumerable<object> GetTopPerformerFromEachDepartment()
        {
            return _employees
                .GroupBy(employee => employee.DepartmentId)
                .Select(group => group
                    .OrderByDescending(employee => employee.PerformanceRating)
                    .First());
        }

        #endregion

        #region Real World Examples

        public IEnumerable<Employee> GetEmployeesEligibleForPromotion()
        {
            return _employees
                .Where(employee =>
                    employee.IsActive &&
                    employee.PerformanceRating >= 4.5 &&
                    employee.Experience >= 5)
                .OrderByDescending(employee => employee.PerformanceRating)
                .ThenByDescending(employee => employee.Experience);
        }

        public IEnumerable<Employee> GetRecentlyJoinedEmployees(int months = 6)
        {
            DateTime joiningDate = DateTime.Now.AddMonths(-months);

            return _employees
                .Where(employee => employee.JoiningDate >= joiningDate)
                .OrderByDescending(employee => employee.JoiningDate);
        }

        public IEnumerable<Employee> GetHighestPaidEmployeeFromEachDepartment()
        {
            return _employees
                .GroupBy(employee => employee.DepartmentId)
                .Select(group => group
                    .OrderByDescending(employee => employee.Salary)
                    .First());
        }

        #endregion
    }
}
