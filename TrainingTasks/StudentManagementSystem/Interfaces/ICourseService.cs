using StudentManagementSystem.Models;

namespace StudentManagementSystem.Interfaces;

public interface ICourseService
{
    #region Basic Queries

    List<Course> GetAllCourses();

    Course? GetCourseById(int id);

    Course? GetCourseByCode(string code);

    #endregion

    #region Filtering


    List<Course> GetCoursesByCreditHours(int creditHours);

    List<Course> SearchCourses(string keyword);

    #endregion

    #region Sorting

    List<Course> GetCoursesOrderedByName();

    List<Course> GetCoursesOrderedByCreditHours();

    #endregion
}