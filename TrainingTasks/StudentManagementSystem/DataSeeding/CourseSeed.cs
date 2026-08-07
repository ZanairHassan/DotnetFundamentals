using System;
using System.Collections.Generic;
using System.Text;

using StudentManagementSystem.Models;

namespace StudentManagementSystem.DataSeeding;

public static class CourseSeed
{
    public static List<Course> Seed()
    {
        return
        [
            new Course
            {
                Id = 1,
                Code = "CS101",
                Name = "Programming Fundamentals",
                CreditHours = 3,
                TotalMarks = 100,
                PassingMarks = 40
            },

            new Course
            {
                Id = 2,
                Code = "CS102",
                Name = "Object Oriented Programming",
                CreditHours = 3,
                TotalMarks = 100,
                PassingMarks = 40
            },

            new Course
            {
                Id = 3,
                Code = "CS103",
                Name = "Database Systems",
                CreditHours = 3,
                TotalMarks = 100,
                PassingMarks = 40
            },

            new Course
            {
                Id = 4,
                Code = "CS104",
                Name = "Data Structures",
                CreditHours = 4,
                TotalMarks = 100,
                PassingMarks = 40
            },

            new Course
            {
                Id = 5,
                Code = "CS105",
                Name = "Operating Systems",
                CreditHours = 4,
                TotalMarks = 100,
                PassingMarks = 40
            }
        ];
    }
}
