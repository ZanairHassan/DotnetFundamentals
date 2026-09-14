using WeeklyAssignment.Models;

namespace WeeklyAssignment.ViewModels.Employees;

public class EmployeeDetailsVM
{
    public Employee Employee { get; set; } = null!;

    public Designation? Designation { get; set; }
}