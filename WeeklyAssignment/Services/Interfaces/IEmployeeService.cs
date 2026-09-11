using WeeklyAssignment.Models;

namespace WeeklyAssignment.Services.Interfaces;

public interface IEmployeeService
{
    IEnumerable<Employee> GetAll();

    Employee? GetById(int id);

    IEnumerable<Employee> Search(string? searchTerm, int? designationId);

    bool Create(Employee employee);

    bool Update(Employee employee);

    bool Delete(int id);

    bool Exists(int id);

    IEnumerable<Designation> GetDesignations();

    bool DesignationExists(int designationId);
}