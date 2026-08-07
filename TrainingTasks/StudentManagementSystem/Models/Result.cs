using StudentManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Models;

public class Result
{
    public int StudentId { get; set; }

    public int TotalObtainedMarks { get; set; }

    public int TotalMarks { get; set; }

    public double Percentage { get; set; }

    public Grade Grade { get; set; }

    public bool IsPassed { get; set; }
}
