using System;
using System.Collections.Generic;
using System.Text;

using StudentManagementSystem.Models;

namespace StudentManagementSystem.DataSeeding;

public static class CourseResultSeed
{
    public static List<CourseResult> Seed(
        List<StudentCourse> registrations,
        List<Course> courses)
    {
        Random random = Random.Shared;

        List<CourseResult> results = new();

        foreach (StudentCourse registration in registrations)
        {
            Course course = courses.First(c => c.Id == registration.CourseId);

            results.Add(new CourseResult
            {
                StudentId = registration.StudentId,
                CourseId = registration.CourseId,
                ObtainedMarks = random.Next(
                    course.PassingMarks,
                    course.TotalMarks + 1)
            });
        }

        return results;
    }
}
