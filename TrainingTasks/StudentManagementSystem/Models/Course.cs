using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Models;

public class Course
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int CreditHours { get; set; }

    public int TotalMarks { get; set; }

    public int PassingMarks { get; set; }
}
