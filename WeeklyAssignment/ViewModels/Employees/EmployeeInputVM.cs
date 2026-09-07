using System.ComponentModel.DataAnnotations;
using WeeklyAssignment.Models;

namespace WeeklyAssignment.ViewModels.Employees;

public class EmployeeInputVM
{
    [Required]
    [StringLength(100)]
    [Display(Name = "Employee Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Range(0, 10_000_000)]
    public decimal Salary { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Joining Date")]
    public DateTime JoiningDate { get; set; }

    [Range(1, int.MaxValue)]
    [Display(Name = "Designation")]
    public int DesignationId { get; set; }

    public IEnumerable<Designation> Designations { get; set; } = [];
}