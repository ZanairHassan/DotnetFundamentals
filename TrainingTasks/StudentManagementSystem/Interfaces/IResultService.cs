using StudentManagementSystem.Enums;
using StudentManagementSystem.Models;

public interface IResultService
{
    Result GetStudentResult(int studentId);

    List<Result> GetAllResults();

    List<Result> GetPassedStudents();

    List<Result> GetFailedStudents();

    List<Result> GetResultsOrderedByPercentage();

    Result? GetTopStudent();

    Result? GetLowestStudent();

    double GetClassAveragePercentage();

    double GetPassPercentage();

    Dictionary<Grade, int> GetGradeDistribution();
}