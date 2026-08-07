using StudentManagementSystem.BusinessLogic;
using StudentManagementSystem.Enums;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services;

public class ResultService : IResultService
{
    private readonly List<Student> _students;

    private readonly List<Course> _courses;

    private readonly List<CourseResult> _courseResults;

    public ResultService(
        List<Student> students,
        List<Course> courses,
        List<CourseResult> courseResults)
    {
        _students = students;
        _courses = courses;
        _courseResults = courseResults;
    }

    #region Read

    public Result GetStudentResult(int studentId)
    {
        try
        {
            return ResultCalculator.CalculateResult(
                studentId,
                _courseResults,
                _courses);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException(
                $"Unable to calculate the result for student with Id {studentId}. One or more course records are missing.",
                ex);
        }
    }
    public List<Result> GetAllResults()
    {
        try
        {
            return _students
                .Select(student =>
                    ResultCalculator.CalculateResult(
                        student.Id,
                        _courseResults,
                        _courses))
                .ToList();
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException(
                "Unable to calculate student results because one or more course records are invalid.",
                ex);
        }
    }


    public List<Result> GetPassedStudents()
    {
        return GetAllResults()
            .Where(result => result.IsPassed)
            .ToList();
    }

    public List<Result> GetFailedStudents()
    {
        return GetAllResults()
            .Where(result => !result.IsPassed)
            .ToList();
    }

    public List<Result> GetResultsOrderedByPercentage()
    {
        return GetAllResults()
            .OrderByDescending(result => result.Percentage)
            .ThenBy(result => result.StudentId)
            .ToList();
    }

    public Result? GetTopStudent()
    {
        return GetAllResults()
            .OrderByDescending(result => result.Percentage)
            .FirstOrDefault();
    }

    public Result? GetLowestStudent()
    {
        return GetAllResults()
            .OrderBy(result => result.Percentage)
            .FirstOrDefault();
    }

    public double GetClassAveragePercentage()
    {
        List<Result> results = GetAllResults();

        if (!results.Any())
        {
            return 0;
        }

        return Math.Round(
            results.Average(result => result.Percentage),
            2);
    }

    public double GetPassPercentage()
    {
        List<Result> results = GetAllResults();

        if (!results.Any())
        {
            return 0;
        }

        int passedStudents = results.Count(result => result.IsPassed);

        return Math.Round(
            (double)passedStudents / results.Count * 100,
            2);
    }

    public Dictionary<Grade, int> GetGradeDistribution()
    {
        return GetAllResults()
            .GroupBy(result => result.Grade)
            .ToDictionary(
                group => group.Key,
                group => group.Count());
    }

    #endregion
}