using StudentManagementSystem.BusinessLogic;
using StudentManagementSystem.Enums;
using StudentManagementSystem.Helpers;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;
using StudentManagementSystem.Strings;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.MenuOptions;

public class StudentMenu
{
    private readonly IStudentService _studentService;

    public StudentMenu(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public void ShowMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            StudentDisplayHelper.ShowStudentOptions();

            int choice = ConsoleHelper.ReadInt(Prompts.EnterChoice);

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    RegisterStudent();
                    break;

                case 2:
                    UpdateStudent();
                    break;

                case 3:
                    DeleteStudent();
                    break;

                case 4:
                    GetStudentById();
                    break;

                case 5:
                    GetStudentByRegistrationNumber();
                    break;

                case 6:
                    DisplayAllStudents();
                    break;

                case 7:
                    DisplayStudentsByDepartment();
                    break;

                case 8:
                    DisplayActiveStudents();
                    break;

                case 0:
                    isRunning = false;
                    break;

                default:
                    ConsoleHelper.PrintError(ErrorMessages.InvalidChoice);
                    ConsoleHelper.Pause();
                    break;
            }
        }
    }

    #region Student Operations

    private void RegisterStudent()
    {
        ConsoleHelper.PrintHeader("Register Student");
        List<Student> objStudents = _studentService.GetAllStudents();

        Student student = new Student();

        student.Id = objStudents.Any()
            ? objStudents.Max(s => s.Id) + 1
            : 1;

        student.RegistrationNumber =
            $"STD-{student.Id:0000}";

        student.FirstName =
            ConsoleHelper.ReadString(Prompts.EnterFirstName);

        student.LastName =
            ConsoleHelper.ReadString(Prompts.EnterLastName);

        student.Age =
            ConsoleHelper.ReadInt(Prompts.EnterAge);

        Console.WriteLine();

        student.Gender = ConsoleHelper.ReadGender();
        student.Email =
            ConsoleHelper.ReadString(Prompts.EnterEmail);

        student.PhoneNumber =
            ConsoleHelper.ReadString(Prompts.EnterPhoneNumber);

        student.DepartmentId =
            ConsoleHelper.ReadInt(Prompts.EnterDepartmentId);

        DateTime currentDateTime = DateTime.Now;

        student.EnrollmentDate = currentDateTime;

        student.IsActive = true;

        student.CreatedAt = currentDateTime;

        student.UpdatedAt = null;

        if (!StudentValidator.ValidateStudent(
                student,
                objStudents,
                out string validationMessage))
        {
            ConsoleHelper.PrintError(validationMessage);

            ConsoleHelper.Pause();

            return;
        }

        Student registeredStudent =
            _studentService.RegisterStudent(student);

        ConsoleHelper.PrintSuccess(
            SuccessMessages.StudentRegistered);

        Console.WriteLine();

        Console.WriteLine($"Student Id : {registeredStudent.Id}");

        Console.WriteLine($"Registration : {registeredStudent.RegistrationNumber}");

        ConsoleHelper.Pause();
    }

    private void UpdateStudent()
    {
        ConsoleHelper.PrintHeader("Update Student");

        int studentId = ConsoleHelper.ReadInt(Prompts.EnterStudentId);

        Student? existingStudent = _studentService.GetStudentById(studentId);

        if (existingStudent is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.StudentNotFound);
            ConsoleHelper.Pause();
            return;
        }

        List<Student> objStudents = _studentService
            .GetAllStudents()
            .Where(s => s.Id != existingStudent.Id)
            .ToList();

        Console.WriteLine();
        Console.WriteLine("Press Enter to keep the existing value.");
        Console.WriteLine();

        string? firstName = ConsoleHelper.ReadOptionalString(
            $"First Name ({existingStudent.FirstName}):\t");

        string? lastName = ConsoleHelper.ReadOptionalString(
            $"Last Name ({existingStudent.LastName}):\t");

        int age = ConsoleHelper.ReadInt(
            $"Age ({existingStudent.Age}):\t");

        Console.WriteLine();

        Gender gender = ConsoleHelper.ReadGender();

        string? email = ConsoleHelper.ReadOptionalString(
            $"Email ({existingStudent.Email}):\t");

        string? phoneNumber = ConsoleHelper.ReadOptionalString($"Phone Number ({existingStudent.PhoneNumber}):\t");

        int departmentId = ConsoleHelper.ReadInt(
            $"Department Id ({existingStudent.DepartmentId}):\t");

        Student updatedStudent = new Student
        {
            Id = existingStudent.Id,

            RegistrationNumber = existingStudent.RegistrationNumber,

            FirstName = ConsoleHelper.KeepExistingValue(
                firstName,
                existingStudent.FirstName),

            LastName = ConsoleHelper.KeepExistingValue(
                lastName,
                existingStudent.LastName),

            Age = age,

            Gender = gender,

            Email = ConsoleHelper.KeepExistingValue(
                email,
                existingStudent.Email),

            PhoneNumber = ConsoleHelper.KeepExistingValue(
                phoneNumber,
                existingStudent.PhoneNumber),

            DepartmentId = departmentId,

            EnrollmentDate = existingStudent.EnrollmentDate,

            IsActive = existingStudent.IsActive,

            CreatedAt = existingStudent.CreatedAt,

            UpdatedAt = DateTime.Now
        };

        if (!StudentValidator.ValidateStudent(
                updatedStudent,
                objStudents,
                out string validationMessage))
        {
            ConsoleHelper.PrintError(validationMessage);
            ConsoleHelper.Pause();
            return;
        }

        Student? student = _studentService.UpdateStudent(updatedStudent);

        if (student is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.StudentNotFound);
        }
        else
        {
            ConsoleHelper.PrintSuccess(SuccessMessages.StudentUpdated);

            Console.WriteLine();

            Console.WriteLine($"Student Id : {student.Id}");
            Console.WriteLine($"Registration Number : {student.RegistrationNumber}");
        }

        ConsoleHelper.Pause();
    }

    private void DeleteStudent()
    {
        ConsoleHelper.PrintHeader("Delete Student");

        int studentId = ConsoleHelper.ReadInt(Prompts.EnterStudentId);

        Student? student = _studentService.GetStudentById(studentId);

        if (student is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.StudentNotFound);
            ConsoleHelper.Pause();
            return;
        }

        StudentDisplayHelper.PrintStudent(student);

        if (!ConsoleHelper.Confirm("Are you sure you want to delete this student?"))
        {
            ConsoleHelper.PrintInformation("Delete operation cancelled.");
            ConsoleHelper.Pause();
            return;
        }

        bool deleted = _studentService.DeleteStudent(studentId);

        if (deleted)
        {
            ConsoleHelper.PrintSuccess(SuccessMessages.StudentDeleted);
        }
        else
        {
            ConsoleHelper.PrintError(ErrorMessages.StudentNotFound);
        }

        ConsoleHelper.Pause();
    }

    private void GetStudentById()
    {
        ConsoleHelper.PrintHeader("Get Student By Id");

        int studentId = ConsoleHelper.ReadInt(Prompts.EnterStudentId);

        Student? student = _studentService.GetStudentById(studentId);

        if (student is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.StudentNotFound);
        }
        else
        {
            StudentDisplayHelper.PrintStudent(student);
        }

        ConsoleHelper.Pause();
    }

    private void GetStudentByRegistrationNumber()
    {
        ConsoleHelper.PrintHeader("Search Student");

        string registrationNumber =
            ConsoleHelper.ReadString(Prompts.EnterRegistrationNumber);

        Student? student =
            _studentService.GetStudentByRegistrationNumber(registrationNumber);

        if (student is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.StudentNotFound);
        }
        else
        {
            StudentDisplayHelper.PrintStudent(student);
        }

        ConsoleHelper.Pause();
    }

    private void DisplayAllStudents()
    {
        ConsoleHelper.PrintHeader("All Students");

        List<Student> students = _studentService.GetAllStudents();

        if (!students.Any())
        {
            ConsoleHelper.PrintInformation("No students found.");
            ConsoleHelper.Pause();
            return;
        }

        StudentDisplayHelper.PrintStudents(students);

        ConsoleHelper.Pause();
    }

    private void DisplayStudentsByDepartment()
    {
        ConsoleHelper.PrintHeader("Students By Department");

        int departmentId = ConsoleHelper.ReadInt(Prompts.EnterDepartmentId);

        List<Student> students =
            _studentService.GetStudentsByDepartment(departmentId);

        if (!students.Any())
        {
            ConsoleHelper.PrintInformation("No students found.");
            ConsoleHelper.Pause();
            return;
        }

        StudentDisplayHelper.PrintStudents(students);

        ConsoleHelper.Pause();
    }

    private void DisplayActiveStudents()
    {
        ConsoleHelper.PrintHeader("Active Students");

        List<Student> students = _studentService.GetActiveStudents();

        if (!students.Any())
        {
            ConsoleHelper.PrintInformation("No active students found.");
            ConsoleHelper.Pause();
            return;
        }

        StudentDisplayHelper.PrintStudents(students);

        ConsoleHelper.Pause();
    }

    #endregion
}
