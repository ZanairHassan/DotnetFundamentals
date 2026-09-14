namespace WeeklyAssignment.Models;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public DateTime JoiningDate { get; set; }

    public int DesignationId { get; set; }

    public Designation? Designation { get; set; }
}