using AdvanceLinqOperations.DataSeed;
using AdvanceLinqOperations.Helpers;
using AdvanceLinqOperations.MenuOptions;
using AdvanceLinqOperations.Models;
using AdvanceLinqOperations.Services;

List<Employee> listEp = EmployeeSeed.SeedEmployees();

List<Department> listDp = DepartmentSeed.SeedDepartments();

EmployeeService empSobj = new EmployeeService(listEp, listDp);

FilteringOptions objFilteringOptions = new FilteringOptions(empSobj);

GroupingOptions objGroupingOptions = new GroupingOptions(empSobj);

AggregationOptions objAggregationOptions = new AggregationOptions(empSobj);

RealWorldExampleOptions objRealWorldExampleOptions = new RealWorldExampleOptions(empSobj);

bool isRunning = true;

while (isRunning)
{
    ConsoleHelper.PrintHeader("ADVANCED LINQ OPERATIONS");

    Console.WriteLine("1. Filtering");
    Console.WriteLine("2. Aggregation");
    Console.WriteLine("3. Grouping");
    Console.WriteLine("4. Real World Examples");

    Console.WriteLine("0. Exit");

    Console.WriteLine(new string('-', 35));

    Console.Write("Select Option:\t");
    int choice = ConsoleHelper.ReadInt();
    switch (choice)
    {
        case 1:
            objFilteringOptions.ShowMenu();
            break;


        case 2:
            objAggregationOptions.ShowMenu();
            break;


        case 3:
            objGroupingOptions.ShowMenu();
            break;

        case 4:
            objRealWorldExampleOptions.ShowMenu();
            break;


        case 0:
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid option.");
            ConsoleHelper.Pause();
            break;
    }
}

Console.Clear();
Console.WriteLine("Application terminated successfully.");
Console.ReadKey();

