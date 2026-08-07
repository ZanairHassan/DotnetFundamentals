using System;
using System.Collections.Generic;
using System.Text;

using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> _students;

    public StudentService(List<Student> students)
    {
        _students = students;
    }

    #region Create

    public Student RegisterStudent(Student student)
    {
        _students.Add(student);

        return student;
    }

    #endregion

    #region Read

    public List<Student> GetAllStudents()
    {
        return _students
        .OrderBy(s => s.FirstName)
        .ThenBy(s => s.LastName)
        .ToList(); ;
    }

    public Student? GetStudentById(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    public Student? GetStudentByRegistrationNumber(string registrationNumber)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            return null;
        }

        return _students.FirstOrDefault(s =>
            s.RegistrationNumber.Equals(
                registrationNumber,
                StringComparison.OrdinalIgnoreCase));
    }

    public List<Student> GetStudentsByDepartment(int departmentId)
    {
        return _students
            .Where(s => s.DepartmentId == departmentId)
            .ToList();
    }

    public List<Student> GetActiveStudents()
    {
        return _students
            .Where(s => s.IsActive)
            .ToList();
    }

    #endregion

    #region Update

    public Student? UpdateStudent(Student student)
    {
        Student? existingStudent = GetStudentById(student.Id);

        if (existingStudent is null)
        {
            return null;
        }

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Age = student.Age;
        existingStudent.Gender = student.Gender;
        existingStudent.Email = student.Email;
        existingStudent.PhoneNumber = student.PhoneNumber;
        existingStudent.DepartmentId = student.DepartmentId;
        existingStudent.EnrollmentDate = student.EnrollmentDate;
        existingStudent.IsActive = student.IsActive;
        existingStudent.UpdatedAt = DateTime.Now;

        return existingStudent;
    }

    #endregion

    #region Delete

    public bool DeleteStudent(int id)
    {
        Student? student = GetStudentById(id);

        if (student is null)
        {
            return false;
        }

        return _students.Remove(student);
    }

    #endregion
}
