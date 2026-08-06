using AdvanceLinqOperations.Helpers;
using AdvanceLinqOperations.Services;

namespace AdvanceLinqOperations.MenuOptions
{
    public class AggregationOptions
    {
        private readonly EmployeeService _employeeService;

        public AggregationOptions(EmployeeService employeeService)
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
                        ShowEmployeeCount();
                        break;

                    case 2:
                        ShowActiveEmployeeCount();
                        break;

                    case 3:
                        ShowTotalPayroll();
                        break;

                    case 4:
                        ShowAverageSalary();
                        break;

                    case 5:
                        ShowMinimumSalary();
                        break;

                    case 6:
                        ShowHighestPerformanceRating();
                        break;

                    case 7:
                        ShowEmployeeNames();
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

        private void ShowEmployeeCount()
        {
            Console.WriteLine($"\nTotal Employees : {_employeeService.GetEmployeeCount()}");
            ConsoleHelper.Pause();
        }

        private void ShowActiveEmployeeCount()
        {
            Console.WriteLine($"\nActive Employees : {_employeeService.GetActiveEmployeeCount()}");
            ConsoleHelper.Pause();
        }

        private void ShowTotalPayroll()
        {
            Console.WriteLine($"\nTotal Payroll : {_employeeService.GetTotalPayroll():C}");
            ConsoleHelper.Pause();
        }

        private void ShowAverageSalary()
        {
            Console.WriteLine($"\nAverage Salary : {_employeeService.GetAverageSalary():C}");
            ConsoleHelper.Pause();
        }

        private void ShowMinimumSalary()
        {
            Console.WriteLine($"\nMinimum Salary : {_employeeService.GetMinimumSalary():C}");
            ConsoleHelper.Pause();
        }

        private void ShowHighestPerformanceRating()
        {
            Console.WriteLine($"\nHighest Performance Rating : {_employeeService.GetHighestPerformanceRating():0.00}");
            ConsoleHelper.Pause();
        }

        private void ShowEmployeeNames()
        {
            Console.WriteLine("\nEmployee Names:");
            Console.WriteLine(_employeeService.GetEmployeeNames());
            ConsoleHelper.Pause();
        }

        #endregion

        #region Helper Methods

        private void Show()
        {
            ConsoleHelper.PrintHeader("LINQ AGGREGATION OPERATIONS");

            Console.WriteLine("1. Employee Count (Count)");
            Console.WriteLine("2. Active Employee Count (LongCount)");
            Console.WriteLine("3. Total Payroll (Sum)");
            Console.WriteLine("4. Average Salary (Average)");
            Console.WriteLine("5. Minimum Salary (Min)");
            Console.WriteLine("6. Highest Performance Rating (Max)");
            Console.WriteLine("7. Employee Names (Aggregate)");
            Console.WriteLine("0. Back");
            Console.WriteLine(new string('-', 35));

            Console.Write("Select Option:\t");
        }

        #endregion
    }
}