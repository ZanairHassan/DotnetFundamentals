using System;
using System.Collections.Generic;
using System.Text;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.BusinessLogic;

public class RegistrationManager
{
    private readonly List<StudentCourse> _registrations;

    public RegistrationManager(List<StudentCourse> registrations)
    {
        _registrations = registrations;
    }

    public bool RegisterCourse(int studentId, int courseId)
    {
        bool alreadyRegistered = _registrations.Any(r => r.StudentId == studentId && r.CourseId == courseId);

        if (alreadyRegistered)
        {
            return false;
        }

        _registrations.Add(new StudentCourse
        {
            StudentId = studentId,
            CourseId = courseId
        });

        return true;
    }

    public bool RemoveCourse(int studentId, int courseId)
    {
        StudentCourse? registration = _registrations.FirstOrDefault(r =>
            r.StudentId == studentId &&
            r.CourseId == courseId);

        if (registration is null)
        {
            return false;
        }

        return _registrations.Remove(registration);
    }

    public List<int> GetStudentCourses(int studentId)
    {
        return _registrations
            .Where(r => r.StudentId == studentId)
            .Select(r => r.CourseId)
            .ToList();
    }

    public List<int> GetCourseStudents(int courseId)
    {
        return _registrations
            .Where(r => r.CourseId == courseId)
            .Select(r => r.StudentId)
            .ToList();
    }

    public List<StudentCourse> GetAllRegistrations()
    {
        return _registrations;
    }
}
