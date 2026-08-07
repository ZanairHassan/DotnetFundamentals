using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services;

public class DepartmentService : IDepartmentService
{
    private readonly List<Department> _departments;

    public DepartmentService(List<Department> departments)
    {
        _departments = departments;
    }

    #region Basic Queries

    public List<Department> GetAllDepartments()
    {
        return _departments
            .OrderBy(department => department.Name)
            .ToList();
    }

    public Department? GetDepartmentById(int id)
    {
        return _departments
            .FirstOrDefault(department =>
                department.Id == id);
    }

    public Department? GetDepartmentByCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }

        return _departments
            .FirstOrDefault(department =>
                department.Code.Equals(
                    code,
                    StringComparison.OrdinalIgnoreCase));
    }

    #endregion

    #region Searching

    public List<Department> SearchDepartments(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return [];
        }

        keyword = keyword.Trim();

        return _departments
            .Where(department =>
                department.Name.Contains(
                    keyword,
                    StringComparison.OrdinalIgnoreCase)
                ||
                department.Code.Contains(
                    keyword,
                    StringComparison.OrdinalIgnoreCase))
            .OrderBy(department => department.Name)
            .ToList();
    }

    #endregion

    #region Sorting

    public List<Department> GetDepartmentsOrderedByName()
    {
        return _departments
            .OrderBy(department => department.Name)
            .ToList();
    }

    public List<Department> GetDepartmentsOrderedByCode()
    {
        return _departments
            .OrderBy(department => department.Code)
            .ToList();
    }

    #endregion

    #region Statistics

    public Department? GetFirstDepartment()
    {
        return _departments
            .OrderBy(department => department.Id)
            .FirstOrDefault();
    }

    public Department? GetLastDepartment()
    {
        return _departments
            .OrderBy(department => department.Id)
            .LastOrDefault();
    }

    #endregion
}