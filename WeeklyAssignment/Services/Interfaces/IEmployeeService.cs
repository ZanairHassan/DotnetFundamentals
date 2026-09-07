using WeeklyAssignment.Models;
using WeeklyAssignment.ViewModels.Employees;

namespace WeeklyAssignment.Services.Interfaces;

public interface IEmployeeService
{
    EmployeeListVM GetEmployeeList(string? searchTerm, int? designationId);

    EmployeeDetailsVM? GetEmployeeDetails(int id);

    Employee? GetById(int id);

    bool Create(Employee employee);

    bool Update(Employee employee);

    bool Delete(int id);

    bool Exists(int id);

    IEnumerable<Designation> GetDesignations();
}