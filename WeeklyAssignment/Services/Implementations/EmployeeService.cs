using WeeklyAssignment.Models;
using WeeklyAssignment.Repositories.Interfaces;
using WeeklyAssignment.Services.Interfaces;
using WeeklyAssignment.ViewModels.Employees;

namespace WeeklyAssignment.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Employee? GetById(int id)
    {
        return _unitOfWork.Employees.GetById(id);
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
        if (_unitOfWork.Designations.GetById(employee.DesignationId) is null)
        {
            return false;
        }

        return _unitOfWork.Employees.Update(employee);
    }

    public bool Delete(int id)
    {
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

    public EmployeeListVM GetEmployeeList(string? searchTerm, int? designationId)
    {
        var employees = _unitOfWork.Employees.GetAll().ToList();

        var designations = _unitOfWork.Designations.GetAll().ToList();

        var totalEmployees = employees.Count;

        var employeeCounts = employees
            .GroupBy(employee => employee.DesignationId)
            .ToDictionary(
                group => group.Key,
                group => group.Count());

        var designationSummaries = designations
            .Select(designation =>
            {
                employeeCounts.TryGetValue(designation.Id, out var employeeCount);

                var percentage = totalEmployees == 0 ? 0 : employeeCount * 100.0 / totalEmployees;

                return new DesignationSummaryVM
                {
                    Id = designation.Id,
                    Name = designation.Name,
                    EmployeeCount = employeeCount,
                    Percentage = percentage
                };
            })
            .ToList();

        var filteredEmployees = employees.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filteredEmployees = filteredEmployees.Where(employee => employee.Name
            .Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || employee.Email
            .Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        if (designationId.HasValue)
        {
            filteredEmployees = filteredEmployees.Where(employee => employee.DesignationId == designationId.Value);
        }

        var designationLookup = designations.ToDictionary(designation => designation.Id, designation => designation.Name);

        var employeeList = filteredEmployees
            .Select(employee => new EmployeeListItemVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                JoiningDate = employee.JoiningDate,
                DesignationName = designationLookup.TryGetValue(employee.DesignationId, out var designationName) ? designationName : "Unknown"
            })
            .ToList();

        return new EmployeeListVM
        {
            Employees = employeeList,

            Designations = designations,

            DesignationSummaries = designationSummaries,

            SearchTerm = searchTerm,

            DesignationId = designationId,

            TotalEmployees = totalEmployees
        };
    }
}