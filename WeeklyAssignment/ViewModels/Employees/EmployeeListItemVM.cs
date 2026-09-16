namespace WeeklyAssignment.ViewModels.Employees;

public class EmployeeListItemVM
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public DateTime JoiningDate { get; set; }

    public string DesignationName { get; set; } = string.Empty;
}