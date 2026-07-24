using EncapsulationProperties;

EmployeeService employeeService = new EmployeeService();
bool exit = false;
while (!exit)
{
    Console.Clear();

    Console.WriteLine("========== Employee Management ==========");
    Console.WriteLine("1. Add Employee");
    Console.WriteLine("2. Get Employee By ID");
    Console.WriteLine("3. Get All Employees");
    Console.WriteLine("4. Update Employee");
    Console.WriteLine("5. Delete Employee");
    Console.WriteLine("6. Exit");
    Console.WriteLine("=========================================");
    Console.Write("Enter your choice:\t");
    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            employeeService.AddEmployee();
            break;

        case "2":
            Console.Write("Enter Employee ID:\t");
            employeeService.GetEmployeeByID(employeeService.ReadInt());
            break;

        case "3":
            employeeService.GetAllEmployees();
            break;

        case "4":
            Console.Write("Enter Employee ID:\t");
            employeeService.UpdateEmployee(employeeService.ReadInt());
            break;

        case "5":
            Console.Write("Enter Employee ID:\t");
            employeeService.DeleteEmployee(employeeService.ReadInt());
            break;

        case "6":
            Console.Write("\nDo you want Exit From the App? (Y/N): ");
            char desire = char.ToUpper(Convert.ToChar(Console.ReadLine()));
            if (desire == 'Y')
            {
                exit = true;
                Console.WriteLine("Application Closed...");
                break;
            }
            else
            {
                exit= false;
                break;
            }

        default:
            Console.WriteLine("Invalid Option.");
            break;
    }

    if (!exit)
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}