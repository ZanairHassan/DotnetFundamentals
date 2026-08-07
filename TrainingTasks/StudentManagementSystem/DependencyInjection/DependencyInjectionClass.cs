using Microsoft.Extensions.DependencyInjection;

using StudentManagementSystem.DataSeeding;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.MenuOptions;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.DependencyInjection;

public static class DependencyInjectionClass
{
    public static IServiceCollection AddStudentManagementSystem(this IServiceCollection services)
    {
        #region Seed Data

        services.AddSingleton<List<Student>>(StudentSeed.Seed());

        services.AddSingleton<List<Department>>(DepartmentSeed.Seed());

        services.AddSingleton<List<Course>>(CourseSeed.Seed());

        services.AddSingleton<List<StudentCourse>>(provider =>
        {
            List<Student> students = provider.GetRequiredService<List<Student>>();

            List<Course> courses = provider.GetRequiredService<List<Course>>();

            return RegistrationSeed.Seed(students, courses);
        });

        services.AddSingleton<List<CourseResult>>(provider =>
        {
            List<StudentCourse> registrations = provider.GetRequiredService<List<StudentCourse>>();

            List<Course> courses = provider.GetRequiredService<List<Course>>();

            return CourseResultSeed.Seed(registrations, courses);
        });

        #endregion

        #region Services

        services.AddSingleton<IStudentService, StudentService>();

        services.AddSingleton<ICourseService, CourseService>();

        services.AddSingleton<IDepartmentService, DepartmentService>();

        services.AddSingleton<IResultService, ResultService>();

        #endregion

        #region Menus

        services.AddSingleton<StudentMenu>();

        services.AddSingleton<CourseMenu>();

        services.AddSingleton<DepartmentMenu>();

        services.AddSingleton<ResultMenu>();

        #endregion

        return services;
    }
}