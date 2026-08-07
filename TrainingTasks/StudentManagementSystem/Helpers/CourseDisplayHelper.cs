using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Helpers
{
    public static class CourseDisplayHelper
    {
        public static void PrintCourse(Course course)
        {
            Console.WriteLine(new string('-', 60));

            Console.WriteLine($"Course Id       : {course.Id}");
            Console.WriteLine($"Course Code     : {course.Code}");
            Console.WriteLine($"Course Name     : {course.Name}");
            Console.WriteLine($"Credit Hours    : {course.CreditHours}");
            Console.WriteLine($"Total Marks     : {course.TotalMarks}");
            Console.WriteLine($"Passing Marks   : {course.PassingMarks}");

            Console.WriteLine(new string('-', 60));
        }

        public static void PrintCourses(IEnumerable<Course> courses)
        {
            List<Course> courseList = courses.ToList();

            foreach (Course course in courseList)
            {
                PrintCourse(course);
            }

            Console.WriteLine();
            Console.WriteLine($"Total Courses : {courseList.Count}");
        }

        public static void ShowCourseOptions()
        {
            ConsoleHelper.PrintHeader("Course Management");

            Console.WriteLine("1. Display All Courses");
            Console.WriteLine("2. Get Course By Id");
            Console.WriteLine("3. Get Course By Code");
            Console.WriteLine("4. Search Course");
            Console.WriteLine("5. Display Courses By Credit Hours");
            Console.WriteLine("6. Display Courses Ordered By Name");
            Console.WriteLine("7. Display Courses Ordered By Credit Hours");
            Console.WriteLine("0. Back");

            Console.WriteLine();
        }
    }
}
