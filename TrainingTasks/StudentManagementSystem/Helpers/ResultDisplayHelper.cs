using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Helpers;

public static class ResultDisplayHelper
{
    public static void PrintResult(Student student, Result result)
    {
        Console.WriteLine(new string('-', 50));

        Console.WriteLine($"Student Id            : {student.Id}");

        Console.WriteLine($"Registration Number   : {student.RegistrationNumber}");

        Console.WriteLine($"Student Name          : {student.FullName}");

        Console.WriteLine($"Total Obtained Marks  : {result.TotalObtainedMarks}");

        Console.WriteLine($"Total Marks           : {result.TotalMarks}");

        Console.WriteLine($"Percentage            : {result.Percentage:F2}%");

        Console.WriteLine($"Grade                 : {result.Grade}");

        Console.WriteLine($"Status                : {(result.IsPassed ? "Passed" : "Failed")}");

        Console.WriteLine(new string('-', 50));
    }

    public static void  PrintResults(
       List<Result> results,
       IStudentService studentService)
    {
        if (!results.Any())
        {
            Console.WriteLine("No results found.");
            return;
        }

        foreach (Result result in results)
        {
            Student? student =
                studentService.GetStudentById(result.StudentId);

            if (student is not null)
            {
                PrintResult(student, result);
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Total Passed Students : {results.Count}");
    }

    public static void PrintClassAverage(double averagePercentage)
    {
        Console.WriteLine(new string('-', 60));

        Console.WriteLine($"Class Average Percentage : {averagePercentage:F2}%");

        Console.WriteLine(new string('-', 60));
    }

    public static void PrintPassPercentage(double passPercentage)
    {
        Console.WriteLine(new string('-', 60));

        Console.WriteLine($"Pass Percentage : {passPercentage:F2}%");

        Console.WriteLine(new string('-', 60));
    }

    public static void PrintGradeDistribution(
        Dictionary<Enums.Grade, int> gradeDistribution)
    {
        Console.WriteLine(new string('-', 60));

        Console.WriteLine("Grade Distribution");

        Console.WriteLine(new string('-', 60));

        foreach (KeyValuePair<Enums.Grade, int> item in gradeDistribution)
        {
            Console.WriteLine($"{item.Key,-10} : {item.Value}");
        }

        Console.WriteLine(new string('-', 60));
    }

    public static void ShowResultOptions()
    {
        Console.Clear();

        ConsoleHelper.PrintHeader("Result Management");

        Console.WriteLine("1. Display All Results");
        Console.WriteLine("2. Display Student Result");
        Console.WriteLine("3. Display Passed Students");
        Console.WriteLine("4. Display Failed Students");
        Console.WriteLine("5. Display Results Ordered By Percentage");
        Console.WriteLine("6. Display Top Student");
        Console.WriteLine("7. Display Lowest Student");
        Console.WriteLine("8. Display Class Average Percentage");
        Console.WriteLine("9. Display Pass Percentage");
        Console.WriteLine("10. Display Grade Distribution");
        Console.WriteLine("0. Back");

        Console.WriteLine();
    }
}