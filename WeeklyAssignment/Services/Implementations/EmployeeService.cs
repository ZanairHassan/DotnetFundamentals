using WeeklyAssignment.Models;
using WeeklyAssignment.Repositories.Interfaces;
using WeeklyAssignment.Services.Interfaces;

namespace WeeklyAssignment.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _unitOfWork.Employees.GetAll();
    }

    public Employee? GetById(int id)
    {
        return _unitOfWork.Employees.GetById(id);
    }

    public IEnumerable<Employee> Search(string? searchTerm, int? designationId)
    {
        IEnumerable<Employee> employees = _unitOfWork.Employees.GetAll();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            employees = employees.Where(employee => employee.Name
            .Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || employee.Email
            .Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        if (designationId.HasValue)
        {
            employees = employees.Where(employee => employee.DesignationId == designationId.Value);
        }

        return employees;
    }

    public bool Create(Employee employee)
    {
        if (_unitOfWork.Designations.GetById(employee.DesignationId) is null)
        {
            return false;
        }

        _unitOfWork.Employees.Add(employee);

        return true;
    }

    public bool Update(Employee employee)
    {
        if (!_unitOfWork.Employees.Exists(employee.Id))
        {
            return false;
        }

        if (_unitOfWork.Designations.GetById(employee.DesignationId) is null)
        {
            return false;
        }

        _unitOfWork.Employees.Update(employee);

        return true;
    }

    public bool Delete(int id)
    {
        if (!_unitOfWork.Employees.Exists(id))
        {
            return false;
        }

        return _unitOfWork.Employees.Delete(id);
    }

    public bool Exists(int id)
    {
        return _unitOfWork.Employees.Exists(id);
    }

    public IEnumerable<Designation> GetDesignations()
    {
        return _unitOfWork.Designations.GetAll();
    }

    public bool DesignationExists(int designationId)
    {
        return _unitOfWork.Designations.GetById(designationId) is not null;
    }
}