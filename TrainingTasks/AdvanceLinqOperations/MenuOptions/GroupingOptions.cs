using AdvanceLinqOperations.Helpers;
using AdvanceLinqOperations.Services;

namespace AdvanceLinqOperations.MenuOptions
{
    public class GroupingOptions
    {
        private readonly EmployeeService _employeeService;

        public GroupingOptions(EmployeeService employeeService)
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
                        ShowEmployeesGroupedByDepartment();
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

        private void ShowEmployeesGroupedByDepartment()
        {
            var groups = _employeeService.GroupEmployeesByDepartment();

            Console.WriteLine();

            foreach (var group in groups)
            {
                Console.WriteLine($"Department Id : {group.Key}");
                Console.WriteLine(new string('-', 35));

                ConsoleHelper.PrintEmployees(group);

                Console.WriteLine();
            }

            ConsoleHelper.Pause();
        }

        #endregion

        #region Helper Methods

        private void Show()
        {
            ConsoleHelper.PrintHeader("LINQ GROUPING OPERATIONS");

            Console.WriteLine("1. Group Employees By Department (GroupBy)");
            Console.WriteLine("0. Back");
            Console.WriteLine(new string('=', 40));

            Console.Write("Select Option:\t");
        }

        #endregion
    }
}