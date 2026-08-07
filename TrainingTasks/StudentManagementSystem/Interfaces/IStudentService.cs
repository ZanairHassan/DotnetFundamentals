using System;
using System.Collections.Generic;
using System.Text;

using StudentManagementSystem.Models;

namespace StudentManagementSystem.Interfaces;

public interface IStudentService
{
    #region Create

    Student RegisterStudent(Student student);
    #endregion

    #region Read

    List<Student> GetAllStudents();

    Student? GetStudentById(int id);

    Student? GetStudentByRegistrationNumber(string registrationNumber);

    List<Student> GetStudentsByDepartment(int departmentId);

    List<Student> GetActiveStudents();

    #endregion

    #region Update

    Student? UpdateStudent(Student student);
    #endregion

    #region Delete

    bool DeleteStudent(int id);

    #endregion
}
