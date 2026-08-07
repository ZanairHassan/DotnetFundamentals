using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Helpers
{
    public static class StudentDisplayHelper
    {
        public static void PrintStudent(Student student)
        {
            Console.WriteLine(new string('-', 40));

            Console.WriteLine($"Student Id           : {student.Id}");

            Console.WriteLine($"Registration Number  : {student.RegistrationNumber}");

            Console.WriteLine($"Name                 : {student.FirstName} {student.LastName}");

            Console.WriteLine($"Age                  : {student.Age}");

            Console.WriteLine($"Gender               : {student.Gender}");

            Console.WriteLine($"Department Id        : {student.DepartmentId}");

            Console.WriteLine($"Email                : {student.Email}");

            Console.WriteLine($"Phone Number         : {student.PhoneNumber}");

            Console.WriteLine($"Enrollment Date      : {student.EnrollmentDate:dd-MMM-yyyy}");

            Console.WriteLine($"Status               : {(student.IsActive ? "Active" : "Inactive")}");

            Console.WriteLine($"Created At           : {student.CreatedAt:dd-MMM-yyyy hh:mm tt}");

            Console.WriteLine($"Updated At           : {(student.UpdatedAt.HasValue ? student.UpdatedAt.Value.ToString("dd-MMM-yyyy hh:mm tt") : "N/A")}");

            Console.WriteLine(new string('-', 40));
        }

        public static void PrintStudents(IEnumerable<Student> students)
        {
            List<Student> courseList = students.ToList();

            foreach (Student student in courseList)
            {
                PrintStudent(student);
            }

            Console.WriteLine();
            Console.WriteLine($"Total Students : {courseList.Count}");
        }

        public static void ShowStudentOptions()
        {
            ConsoleHelper.PrintHeader("Student Management");

            Console.WriteLine("1. Register Student");
            Console.WriteLine("2. Update Student");
            Console.WriteLine("3. Delete Student");
            Console.WriteLine("4. Get Student By Id");
            Console.WriteLine("5. Get Student By Registration Number");
            Console.WriteLine("6. Display All Students");
            Console.WriteLine("7. Display Students By Department");
            Console.WriteLine("8. Display Active Students");
            Console.WriteLine("0. Back");

            Console.WriteLine();
        }
    }
}
