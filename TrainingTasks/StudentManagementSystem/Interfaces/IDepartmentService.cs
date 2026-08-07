using StudentManagementSystem.Models;

namespace StudentManagementSystem.Interfaces;

public interface IDepartmentService
{
    #region Basic Queries

    List<Department> GetAllDepartments();

    Department? GetDepartmentById(int id);

    Department? GetDepartmentByCode(string code);

    #endregion

    #region Searching

    List<Department> SearchDepartments(string keyword);

    #endregion

    #region Sorting

    List<Department> GetDepartmentsOrderedByName();

    List<Department> GetDepartmentsOrderedByCode();

    #endregion

    #region Statistics

    Department? GetFirstDepartment();

    Department? GetLastDepartment();

    #endregion
}