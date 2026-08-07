using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services;

public class CourseService : ICourseService
{
    private readonly List<Course> _courses;

    public CourseService(List<Course> courses)
    {
        _courses = courses;
    }

    #region Basic Queries

    public List<Course> GetAllCourses()
    {
        return _courses
            .OrderBy(course => course.Name)
            .ToList();
    }

    public Course? GetCourseById(int id)
    {
        return _courses
            .FirstOrDefault(course => course.Id == id);
    }

    public Course? GetCourseByCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }

        return _courses
            .FirstOrDefault(course =>
                course.Code.Equals(
                    code,
                    StringComparison.OrdinalIgnoreCase));
    }

    #endregion

    #region Filtering


    public List<Course> GetCoursesByCreditHours(int creditHours)
    {
        if (creditHours <= 0)
        {
            return [];
        }

        return _courses
            .Where(course => course.CreditHours == creditHours)
            .OrderBy(course => course.Name)
            .ToList();
    }

    public List<Course> SearchCourses(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return [];
        }

        return _courses
            .Where(course =>
                course.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                course.Code.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .OrderBy(course => course.Name)
            .ToList();
    }

    #endregion

    #region Sorting

    public List<Course> GetCoursesOrderedByName()
    {
        return _courses
            .OrderBy(course => course.Name)
            .ToList();
    }

    public List<Course> GetCoursesOrderedByCreditHours()
    {
        return _courses
            .OrderByDescending(course => course.CreditHours)
            .ThenBy(course => course.Name)
            .ToList();
    }

    #endregion

}