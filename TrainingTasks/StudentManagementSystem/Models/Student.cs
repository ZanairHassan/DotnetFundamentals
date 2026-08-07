using StudentManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Models;

public class Student
{
    public int Id { get; set; }

    public string RegistrationNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; }

    public Gender Gender { get; set; } 

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public bool IsActive { get; set; }

    public string FullName => $"{FirstName} {LastName}";
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
