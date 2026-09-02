using WeeklyAssignment.Models;

namespace WeeklyAssignment.ViewModels.Employees;

public class EmployeeListVM
{
    public IEnumerable<EmployeeListItemVM> Employees { get; set; } = [];

    public IEnumerable<Designation> Designations { get; set; } = [];

    public IEnumerable<DesignationSummaryVM> DesignationSummaries { get; set; } = [];

    public string? SearchTerm { get; set; }

    public int? DesignationId { get; set; }

    public int TotalEmployees { get; set; }
}