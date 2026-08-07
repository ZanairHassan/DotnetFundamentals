using Microsoft.Extensions.DependencyInjection;
using StudentManagementSystem.DependencyInjection;
using StudentManagementSystem.Helpers;
using StudentManagementSystem.MenuOptions;

Console.Title = "Student Management System";

ConsoleHelper.LogIn();

ServiceCollection services = new();

services.AddStudentManagementSystem();

ServiceProvider provider = services.BuildServiceProvider();

StudentMenu studentMenu = provider.GetRequiredService<StudentMenu>();

CourseMenu courseMenu = provider.GetRequiredService<CourseMenu>();

DepartmentMenu departmentMenu = provider.GetRequiredService<DepartmentMenu>();

ResultMenu resultMenu = provider.GetRequiredService<ResultMenu>();

bool isRunning = true;

while (isRunning)
{
    Console.Clear();

    Console.WriteLine("========================================");
    Console.WriteLine("      Student Management System");
    Console.WriteLine("========================================");
    Console.WriteLine();
    Console.WriteLine("1. Student Management");
    Console.WriteLine("2. Course Management");
    Console.WriteLine("3. Department Management");
    Console.WriteLine("4. Result Management");
    Console.WriteLine("0. Exit");
    Console.WriteLine();

    int choice = ConsoleHelper.ReadInt("Select an option: ");

    switch (choice)
    {
        case 1:
            studentMenu.ShowMenu();
            break;

        case 2:
            courseMenu.ShowMenu();
            break;

        case 3:
            departmentMenu.ShowMenu();
            break;

        case 4:
            resultMenu.ShowMenu();
            break;

        case 0:
            isRunning = false;
            break;

        default:
            ConsoleHelper.PrintError("Invalid choice.");
            ConsoleHelper.Pause();
            break;
    }
}

Console.WriteLine();
Console.WriteLine("Application terminated. Press any key to close...");
Console.ReadKey();