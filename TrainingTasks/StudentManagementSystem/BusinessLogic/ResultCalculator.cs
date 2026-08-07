using System;
using System.Collections.Generic;
using System.Text;

using StudentManagementSystem.Enums;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.BusinessLogic;

public static class ResultCalculator
{
    public static Result CalculateResult(
       int studentId,
       List<CourseResult> courseResults,
       List<Course> courses)
    {
        try
        {
            List<CourseResult> studentResults = courseResults
                .Where(r => r.StudentId == studentId)
                .ToList();

            int totalObtainedMarks = studentResults
                .Sum(r => r.ObtainedMarks);

            int totalMarks = studentResults
                .Join(courses,
                    result => result.CourseId,
                    course => course.Id,
                    (result, course) => course.TotalMarks)
                .Sum();

            double percentage = totalMarks == 0
                ? 0
                : Math.Round(
                    (double)totalObtainedMarks / totalMarks * 100,
                    2);

            Dictionary<int, Course> courseLookup =
                courses.ToDictionary(course => course.Id);

            bool isPassed = !studentResults.Any(result =>
                result.ObtainedMarks <
                courseLookup[result.CourseId].PassingMarks);

            Grade grade = GetGrade(percentage);

            return new Result
            {
                StudentId = studentId,
                TotalObtainedMarks = totalObtainedMarks,
                TotalMarks = totalMarks,
                Percentage = percentage,
                Grade = grade,
                IsPassed = isPassed
            };
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException(
                "Duplicate course IDs were found while calculating the result.",
                ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw new InvalidOperationException(
                $"A course referenced in the student's results does not exist.",
                ex);
        }
    }

    private static Grade GetGrade(double percentage)
    {
        if (percentage >= 90)
            return Grade.APlus;

        if (percentage >= 80)
            return Grade.A;

        if (percentage >= 70)
            return Grade.B;

        if (percentage >= 60)
            return Grade.C;

        if (percentage >= 50)
            return Grade.D;

        return Grade.F;
    }
}
