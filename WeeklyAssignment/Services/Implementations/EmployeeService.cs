using WeeklyAssignment.Models;
using WeeklyAssignment.Repositories.Interfaces;
using WeeklyAssignment.Services.Interfaces;

namespace WeeklyAssignment.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDesignationRepository _designationRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IDesignationRepository designationRepository)
    {
        _employeeRepository = employeeRepository;
        _designationRepository = designationRepository;
    }

    public IEnumerable<Employee> GetAll()
    {
        return _employeeRepository.GetAll();
    }

    public Employee? GetById(int id)
    {
        return _employeeRepository.GetById(id);
    }

    public IEnumerable<Employee> Search(string? searchTerm, int? designationId)
    {
        IEnumerable<Employee> employees = _employeeRepository.GetAll();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            employees = employees.Where(employee => employee.Name
            .Contains(searchTerm,StringComparison.OrdinalIgnoreCase) || employee.Email
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
        if (_designationRepository.GetById(employee.DesignationId) is null)
        {
            return false;
        }

        _employeeRepository.Add(employee);

        return true;
    }

  public bool Update(Employee employee)
{
    if (!_employeeRepository.Exists(employee.Id))
    {
        return false;
    }

    if (_designationRepository.GetById(employee.DesignationId) is null)
    {
        return false;
    }

    _employeeRepository.Update(employee);

    return true;
}

    public bool Delete(int id)
    {
        if (!_employeeRepository.Exists(id))
        {
            return false;
        }

        return _employeeRepository.Delete(id);
    }

    public bool Exists(int id)
    {
        return _employeeRepository.Exists(id);
    }

    public IEnumerable<Designation> GetDesignations()
    {
        return _designationRepository.GetAll();
    }

    public bool DesignationExists(int designationId)
    {
        return _designationRepository.GetById(designationId) is not null;
    }
}