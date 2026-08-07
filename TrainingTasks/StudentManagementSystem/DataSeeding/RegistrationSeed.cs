using System;
using System.Collections.Generic;
using System.Text;

using StudentManagementSystem.Models;

namespace StudentManagementSystem.DataSeeding;

public static class RegistrationSeed
{
    public static List<StudentCourse> Seed(
        List<Student> students,
        List<Course> courses)
    {
        Random random = Random.Shared;

        List<StudentCourse> registrations = new();

        foreach (Student student in students)
        {
            int totalCourses = random.Next(3, 6);

            List<int> assignedCourseIds = courses
                .OrderBy(x => random.Next())
                .Take(totalCourses)
                .Select(x => x.Id)
                .ToList();

            foreach (int courseId in assignedCourseIds)
            {
                registrations.Add(new StudentCourse
                {
                    StudentId = student.Id,
                    CourseId = courseId
                });
            }
        }

        return registrations;
    }
}
