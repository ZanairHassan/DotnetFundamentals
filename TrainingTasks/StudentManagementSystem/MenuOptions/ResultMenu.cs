using StudentManagementSystem.Enums;
using StudentManagementSystem.Helpers;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;
using StudentManagementSystem.Strings;

namespace StudentManagementSystem.MenuOptions;

public class ResultMenu
{
    private readonly IStudentService _studentService;
    private readonly IResultService _resultService;

    public ResultMenu(
        IResultService resultService,
        IStudentService studentService)
    {
        _resultService = resultService;
        _studentService = studentService;
    }

    public void ShowMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            ResultDisplayHelper.ShowResultOptions();
            int choice = ConsoleHelper.ReadInt(Prompts.EnterChoice);

            Console.Clear();

            switch (choice)
            {
                case 1:
                    DisplayAllResults();
                    break;

                case 2:
                    DisplayStudentResult();
                    break;

                case 3:
                    DisplayPassedStudents();
                    break;

                case 4:
                    DisplayFailedStudents();
                    break;

                case 5:
                    DisplayResultsOrderedByPercentage();
                    break;

                case 6:
                    DisplayTopStudent();
                    break;

                case 7:
                    DisplayLowestStudent();
                    break;

                case 8:
                    DisplayClassAveragePercentage();
                    break;

                case 9:
                    DisplayPassPercentage();
                    break;

                case 10:
                    DisplayGradeDistribution();
                    break;

                case 0:
                    isRunning = false;
                    break;

                default:
                    ConsoleHelper.PrintError(ErrorMessages.InvalidChoice);
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    private void DisplayAllResults()
    {
        ConsoleHelper.PrintHeader("All Results");

        List<Result> results =
            _resultService.GetAllResults();

        ResultDisplayHelper.PrintResults(
            results,
            _studentService);

        ConsoleHelper.Pause();
    }

    private void DisplayStudentResult()
    {
        ConsoleHelper.PrintHeader("Student Result");

        int studentId =
            ConsoleHelper.ReadInt(Prompts.EnterStudentId);

        Student? student =
            _studentService.GetStudentById(studentId);

        if (student is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.StudentNotFound);
            ConsoleHelper.Pause();
            return;
        }

        Result result =
            _resultService.GetStudentResult(studentId);

        ResultDisplayHelper.PrintResult(student, result);

        ConsoleHelper.Pause();
    }

    private void DisplayPassedStudents()
    {
        ConsoleHelper.PrintHeader("Passed Students");

        List<Result> results =
            _resultService.GetPassedStudents();

        ResultDisplayHelper.PrintResults(
            results,
            _studentService);

        ConsoleHelper.Pause();
    }

    private void DisplayFailedStudents()
    {
        ConsoleHelper.PrintHeader("Failed Students");

        List<Result> results =
            _resultService.GetFailedStudents();

        ResultDisplayHelper.PrintResults(
            results,
            _studentService);

        ConsoleHelper.Pause();
    }

    private void DisplayResultsOrderedByPercentage()
    {
        ConsoleHelper.PrintHeader("Results Ordered By Percentage");

        List<Result> results =
            _resultService.GetResultsOrderedByPercentage();

        ResultDisplayHelper.PrintResults(
            results,
            _studentService);

        ConsoleHelper.Pause();
    }

    private void DisplayTopStudent()
    {
        ConsoleHelper.PrintHeader("Top Student");

        Result? result = _resultService.GetTopStudent();

        if (result is null)
        {
            ConsoleHelper.PrintInformation("No results found.");
            ConsoleHelper.Pause();
            return;
        }

        Student? student =
            _studentService.GetStudentById(result.StudentId);

        if (student is not null)
        {
            ResultDisplayHelper.PrintResult(student, result);
        }

        ConsoleHelper.Pause();
    }

    private void DisplayLowestStudent()
    {
        ConsoleHelper.PrintHeader("Lowest Student");

        Result? result = _resultService.GetLowestStudent();

        if (result is null)
        {
            ConsoleHelper.PrintInformation("No results found.");
            ConsoleHelper.Pause();
            return;
        }

        Student? student =
            _studentService.GetStudentById(result.StudentId);

        if (student is not null)
        {
            ResultDisplayHelper.PrintResult(student, result);
        }

        ConsoleHelper.Pause();
    }

    private void DisplayClassAveragePercentage()
    {
        ConsoleHelper.PrintHeader("Class Average Percentage");

        double averagePercentage =
            _resultService.GetClassAveragePercentage();

        ResultDisplayHelper.PrintClassAverage(averagePercentage);

        ConsoleHelper.Pause();
    }

    private void DisplayPassPercentage()
    {
        ConsoleHelper.PrintHeader("Pass Percentage");

        double passPercentage =
            _resultService.GetPassPercentage();

        ResultDisplayHelper.PrintPassPercentage(passPercentage);

        ConsoleHelper.Pause();
    }

    private void DisplayGradeDistribution()
    {
        ConsoleHelper.PrintHeader("Grade Distribution");

        Dictionary<Grade, int> gradeDistribution =
            _resultService.GetGradeDistribution();

        ResultDisplayHelper.PrintGradeDistribution(
            gradeDistribution);

        ConsoleHelper.Pause();
    }
}