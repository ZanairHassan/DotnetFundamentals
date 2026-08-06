using AdvanceLinqOperations.Helpers;
using AdvanceLinqOperations.Models;
using AdvanceLinqOperations.Services;
using System.Xml;

namespace AdvanceLinqOperations.MenuOptions
{
    public class FilteringOptions
    {
        private readonly EmployeeService _employeeService;

        public FilteringOptions(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void ShowMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Show();
                int choice = ConsoleHelper.ReadInt();

                switch (choice)
                {
                    case 1:
                        ShowAllEmployees();
                        break;

                    case 2:
                        ShowActiveEmployees();
                        break;

                    case 3:
                        ShowInactiveEmployees();
                        break;

                    case 4:
                        ShowEmployeesByCity();
                        break;

                    case 5:
                        ShowEmployeesByDepartment();
                        break;

                    case 6:
                        ShowEmployeesAboveSalary();
                        break;

                    case 7:
                        ShowEmployeesBelowSalary();
                        break;

                    case 8:
                        ShowEmployeesByExperience();
                        break;

                    case 9:
                        ShowEmployeesByAgeRange();
                        break;

                    case 10:
                        ShowEmployeesJoinedAfter();
                        break;

                    case 11:
                        ShowTopPerformers();
                        break;

                    case 12:
                        SearchEmployees();
                        break;

                    case 0:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Option.");
                        ConsoleHelper.Pause();
                        break;
                }
            }
        }

        #region Private Methods

        private void ShowAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();

            ConsoleHelper.PrintEmployees(employees);

        }

        private void ShowActiveEmployees()
        {
            var employees = _employeeService.GetActiveEmployees();

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowInactiveEmployees()
        {
            var employees = _employeeService.GetInactiveEmployees();

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowEmployeesByCity()
        {
            Console.Write("Enter City :\t");
            string city = Console.ReadLine()!;

            var employees = _employeeService.GetEmployeesByCity(city);

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowEmployeesByDepartment()
        {
            Console.Write("Department Id :\t");
            int departmentId = ConsoleHelper.ReadInt();

            var employees = _employeeService.GetEmployeesByDepartment(departmentId);

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowEmployeesAboveSalary()
        {
            Console.Write("Salary :\t");
            decimal salary = ConsoleHelper.ReadDecimal();

            var employees = _employeeService.GetEmployeesAboveSalary(salary);

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowEmployeesBelowSalary()
        {
            Console.Write("Salary :\t");
            decimal salary = ConsoleHelper. ReadDecimal();

            var employees = _employeeService.GetEmployeesBelowSalary(salary);

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowEmployeesByExperience()
        {
            Console.Write("Minimum Experience :\t");
            int experience = ConsoleHelper.ReadInt();

            var employees = _employeeService.GetEmployeesByExperience(experience);

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowEmployeesByAgeRange()
        {
            Console.Write("Minimum Age :\t");
            int minAge = ConsoleHelper.ReadInt();

            Console.Write("Maximum Age :\t");
            int maxAge = ConsoleHelper.ReadInt();

            var employees = _employeeService.GetEmployeesByAgeRange(minAge, maxAge);

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowEmployeesJoinedAfter()
        {
            Console.Write("Joining Date (yyyy-MM-dd):\t");

            DateTime joiningDate = ConsoleHelper.ReadDate();

            var employees = _employeeService.GetEmployeesJoinedAfter(joiningDate);

            ConsoleHelper.PrintEmployees(employees);
        }

        private void ShowTopPerformers()
        {
            Console.Write("Minimum Rating :\t");

            double rating = ConsoleHelper.ReadDouble();

            var employees = _employeeService.GetTopPerformers(rating);

            ConsoleHelper.PrintEmployees(employees);
        }

        private void SearchEmployees()
        {
            Console.Write("Keyword :\t");

            string keyword = Console.ReadLine()!;

            var employees = _employeeService.SearchEmployees(keyword);

            ConsoleHelper.PrintEmployees(employees);
        }

        #endregion

        #region Helper Methods

        private void Show()
        {

            ConsoleHelper.PrintHeader("LINQ FILTERING OPERATIONS");
            Console.WriteLine("1. Show All Employees");
            Console.WriteLine("2. Active Employees");
            Console.WriteLine("3. Inactive Employees");
            Console.WriteLine("4. Employees By City");
            Console.WriteLine("5. Employees By Department");
            Console.WriteLine("6. Employees Above Salary");
            Console.WriteLine("7. Employees Below Salary");
            Console.WriteLine("8. Employees By Experience");
            Console.WriteLine("9. Employees By Age Range");
            Console.WriteLine("10. Employees Joined After Date");
            Console.WriteLine("11. Top Performers");
            Console.WriteLine("12. Search Employee");
            Console.WriteLine("0. Back");
            Console.WriteLine("========================================");

            Console.Write("Select Option :\t");
        }
       

        #endregion
    }
}