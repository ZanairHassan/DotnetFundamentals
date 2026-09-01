using WeeklyAssignment.Models;

namespace WeeklyAssignment.ViewModels.Employees;

public class EmployeeListVM
{
    public IEnumerable<Employee> Employees { get; set; } = [];

    public IEnumerable<Designation> Designations { get; set; } = [];

    public string? SearchTerm { get; set; }

    public int? DesignationId { get; set; }
}