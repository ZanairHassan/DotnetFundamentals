using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Strings;

public static class ErrorMessages
{
    #region General

    public const string InvalidChoice =
        "Invalid option selected.";

    public const string InvalidInput =
        "Invalid input.";

    public const string RecordNotFound =
        "Record not found.";

    #endregion

    #region Student

    public const string StudentNotFound =
        "Student not found.";

    public const string StudentAlreadyExists =
        "Student already exists.";

    public const string InvalidStudentAge =
        "Student age must be between 18 and 60.";

    public const string InvalidEmail =
        "Please enter a valid email address.";

    public const string InvalidPhoneNumber =
        "Please enter a valid phone number.";

    public const string DuplicateRegistrationNumber =
        "Registration number already exists.";

    public const string DuplicateEmail =
        "Email already exists.";

    #endregion

    #region Department

    public const string DepartmentNotFound =
        "Department not found.";

    public const string DepartmentAlreadyExists =
        "Department already exists.";

    #endregion

    #region Course

    public const string CourseNotFound =
        "Course not found.";

    public const string CourseAlreadyExists =
        "Course already exists.";

    #endregion

    #region Registration

    public const string StudentAlreadyRegistered =
        "Student is already registered in this course.";

    public const string RegistrationNotFound =
        "Course registration not found.";

    #endregion

    #region Result

    public const string ResultNotFound =
        "Result not found.";

    public const string InvalidMarks =
        "Obtained marks are invalid.";

    #endregion
}
