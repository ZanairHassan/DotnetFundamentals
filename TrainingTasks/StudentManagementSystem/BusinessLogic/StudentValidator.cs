using System;
using System.Collections.Generic;
using System.Text;

using StudentManagementSystem.Models;

namespace StudentManagementSystem.BusinessLogic;

public static class StudentValidator
{
    public static bool IsValidAge(int age)
    {
        return age >= 18 && age <= 60;
    }

    public static bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email)
               && email.Contains('@')
               && email.Contains('.');
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrWhiteSpace(phoneNumber)
               && phoneNumber.Length >= 11;
    }

    public static bool IsDuplicateRegistrationNumber(
        List<Student> students,
        string registrationNumber)
    {
        return students.Any(s =>
            s.RegistrationNumber.Equals(
                registrationNumber,
                StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsDuplicateEmail(
        List<Student> students,
        string email)
    {
        return students.Any(s =>
            s.Email.Equals(
                email,
                StringComparison.OrdinalIgnoreCase));
    }

    public static bool ValidateStudent(
        Student student,
        List<Student> students,
        out string message)
    {
        if (!IsValidAge(student.Age))
        {
            message = "Invalid student age.";
            return false;
        }

        if (!IsValidEmail(student.Email))
        {
            message = "Invalid email address.";
            return false;
        }

        if (!IsValidPhoneNumber(student.PhoneNumber))
        {
            message = "Invalid phone number.";
            return false;
        }

        if (IsDuplicateRegistrationNumber(students, student.RegistrationNumber))
        {
            message = "Registration number already exists.";
            return false;
        }

        if (IsDuplicateEmail(students, student.Email))
        {
            message = "Email already exists.";
            return false;
        }

        message = "Validation successful.";
        return true;
    }
}
