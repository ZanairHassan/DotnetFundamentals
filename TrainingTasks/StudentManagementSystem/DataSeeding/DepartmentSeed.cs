using System;
using System.Collections.Generic;
using System.Text;

using StudentManagementSystem.Models;

namespace StudentManagementSystem.DataSeeding;

public static class DepartmentSeed
{
    public static List<Department> Seed()
    {
        return
        [
            new Department { Id = 1, Name = "Computer Science", Code = "CS" },
            new Department { Id = 2, Name = "Software Engineering", Code = "SE" },
            new Department { Id = 3, Name = "Information Technology", Code = "IT" },
            new Department { Id = 4, Name = "Artificial Intelligence", Code = "AI" },
            new Department { Id = 5, Name = "Cyber Security", Code = "CY" }
        ];
    }
}
