using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Strings;

public static class SuccessMessages
{
    #region General

    public const string Welcome =
        "Welcome to Student Management System";

    public const string Exit =
        "Application terminated successfully.";

    public const string PressAnyKey =
        "Press any key to continue...";

    public const string Loading =
        "Loading...";

    #endregion

    #region Student

    public const string StudentRegistered =
        "Student registered successfully.";

    public const string StudentUpdated =
        "Student updated successfully.";

    public const string StudentDeleted =
        "Student deleted successfully.";

    public const string StudentFound =
        "Student found successfully.";

    #endregion

    #region Department

    public const string DepartmentAdded =
        "Department added successfully.";

    public const string DepartmentUpdated =
        "Department updated successfully.";

    public const string DepartmentDeleted =
        "Department deleted successfully.";

    #endregion

    #region Course

    public const string CourseAdded =
        "Course added successfully.";

    public const string CourseUpdated =
        "Course updated successfully.";

    public const string CourseDeleted =
        "Course deleted successfully.";

    #endregion

    #region Registration

    public const string CourseAssigned =
        "Course assigned successfully.";

    public const string CourseRemoved =
        "Course removed successfully.";

    #endregion

    #region Result

    public const string ResultCalculated =
        "Result calculated successfully.";

    public const string MarksUpdated =
        "Marks updated successfully.";

    #endregion
}
