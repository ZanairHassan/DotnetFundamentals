using AdvanceLinqOperations.Helpers;
using AdvanceLinqOperations.Services;

namespace AdvanceLinqOperations.MenuOptions
{
    public class RealWorldExampleOptions
    {
        private readonly EmployeeService _employeeService;

        public RealWorldExampleOptions(EmployeeService employeeService)
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
                        ShowEmployeesEligibleForPromotion();
                        break;

                    case 2:
                        ShowRecentlyJoinedEmployees();
                        break;

                    case 3:
                        ShowHighestPaidEmployeeFromEachDepartment();
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

        private void ShowEmployeesEligibleForPromotion()
        {
            ConsoleHelper.PrintEmployees(
                _employeeService.GetEmployeesEligibleForPromotion());

            ConsoleHelper.Pause();
        }

        private void ShowRecentlyJoinedEmployees()
        {
            Console.Write("Enter number of months:\t");

            int months = ConsoleHelper.ReadInt();

            ConsoleHelper.PrintEmployees(
                _employeeService.GetRecentlyJoinedEmployees(months));

            ConsoleHelper.Pause();
        }

        private void ShowHighestPaidEmployeeFromEachDepartment()
        {
            ConsoleHelper.PrintEmployees(
                _employeeService.GetHighestPaidEmployeeFromEachDepartment());

            ConsoleHelper.Pause();
        }

        #endregion

        #region Helper Methods

        private void Show()
        {
            ConsoleHelper.PrintHeader("LINQ REAL WORLD EXAMPLES");

            Console.WriteLine("1. Employees Eligible For Promotion");
            Console.WriteLine("2. Recently Joined Employees");
            Console.WriteLine("3. Highest Paid Employee From Each Department");
            Console.WriteLine("0. Back");
            Console.WriteLine(new string('-', 35));

            Console.Write("Select Option:\t");
        }

        #endregion
    }
}