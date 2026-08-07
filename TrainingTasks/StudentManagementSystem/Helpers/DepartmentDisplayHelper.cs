using StudentManagementSystem.Models;

namespace StudentManagementSystem.Helpers;

public static class DepartmentDisplayHelper
{
    public static void PrintDepartment(Department department)
    {
        Console.WriteLine(new string('-', 60));

        Console.WriteLine($"Department Id   : {department.Id}");
        Console.WriteLine($"Department Code : {department.Code}");
        Console.WriteLine($"Department Name : {department.Name}");

        Console.WriteLine(new string('-', 60));
    }

    public static void PrintDepartments(IEnumerable<Department> departments)
    {
        List<Department> departmentList = departments.ToList();

        foreach (Department department in departmentList)
        {
            PrintDepartment(department);
        }

        Console.WriteLine();
        Console.WriteLine($"Total Departments : {departmentList.Count}");
    }

    public static void ShowDepartmentOptions()
    {
        Console.Clear();

        ConsoleHelper.PrintHeader("Department Management");

        Console.WriteLine("1. Display All Departments");
        Console.WriteLine("2. Get Department By Id");
        Console.WriteLine("3. Get Department By Code");
        Console.WriteLine("4. Search Department");
        Console.WriteLine("5. Display Departments Ordered By Name");
        Console.WriteLine("6. Display Departments Ordered By Code");
        Console.WriteLine("7. Display First Department");
        Console.WriteLine("8. Display Last Department");
        Console.WriteLine("0. Back");

        Console.WriteLine();
    }
}