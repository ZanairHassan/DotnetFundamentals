namespace WeeklyAssignment.ViewModels.Employees;

public class DesignationSummaryVM
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int EmployeeCount { get; set; }

    public double Percentage { get; set; }
}