using ClassesObjects;
using UtilityLibrary;

Console.WriteLine("Hello, World!");
UserService userService = new UserService();

bool exit = false;

while (!exit)
{
    Console.Clear();

    Console.WriteLine("===================================");
    Console.WriteLine("       USER MANAGEMENT SYSTEM");
    Console.WriteLine("===================================");
    Console.WriteLine("1. Add / Update User");
    Console.WriteLine("2. Get All Users");
    Console.WriteLine("3. Search User");
    Console.WriteLine("4. Delete User");
    Console.WriteLine("5. Exit");
    Console.WriteLine("===================================");

    Console.Write("Enter your choice: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            userService.AddOrUpdateUser();
            break;

        case "2":
            Console.Clear();
            userService.GetAllUsers();
            break;

        case "3":
            Console.Clear();
            userService.SearchUserById();
            break;

        case "4":
            Console.Clear();
            userService.DeleteUser();
            break;

        case "5":
            exit = true;
            Console.WriteLine("Thank you for using the system.");
            Loggings.MessageLog("APP TERMINATED");
            continue;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }

    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey();
}
        
    
