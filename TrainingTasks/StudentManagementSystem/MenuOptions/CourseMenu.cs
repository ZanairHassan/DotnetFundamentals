using StudentManagementSystem.Helpers;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;
using StudentManagementSystem.Strings;

namespace StudentManagementSystem.MenuOptions;

public class CourseMenu
{
    private readonly ICourseService _courseService;

    public CourseMenu(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public void ShowMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            CourseDisplayHelper.ShowCourseOptions();

            Console.WriteLine();

            int choice = ConsoleHelper.ReadInt(Prompts.EnterChoice);

            switch (choice)
            {
                case 1:
                    DisplayAllCourses();
                    break;

                case 2:
                    GetCourseById();
                    break;

                case 3:
                    GetCourseByCode();
                    break;

                case 4:
                    SearchCourse();
                    break;

                case 5:
                    DisplayCoursesByCreditHours();
                    break;

                case 6:
                    DisplayCoursesOrderedByName();
                    break;

                case 7:
                    DisplayCoursesOrderedByCreditHours();
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

    private void DisplayAllCourses()
    {
        ConsoleHelper.PrintHeader("All Courses");

        List<Course> courses = _courseService.GetAllCourses();

        if (!courses.Any())
        {
            ConsoleHelper.PrintInformation("No courses found.");
            ConsoleHelper.Pause();
            return;
        }

        CourseDisplayHelper.PrintCourses(courses);

        ConsoleHelper.Pause();
    }

    private void GetCourseById()
    {
        ConsoleHelper.PrintHeader("Get Course By Id");

        int courseId = ConsoleHelper.ReadInt(Prompts.EnterCourseId);

        Course? course = _courseService.GetCourseById(courseId);

        if (course is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.CourseNotFound);
        }
        else
        {
            CourseDisplayHelper.PrintCourse(course);
        }

        ConsoleHelper.Pause();
    }

    private void GetCourseByCode()
    {
        ConsoleHelper.PrintHeader("Get Course By Code");

        string courseCode =
            ConsoleHelper.ReadString(Prompts.EnterCourseCode);

        Course? course =
            _courseService.GetCourseByCode(courseCode);

        if (course is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.CourseNotFound);
        }
        else
        {
            CourseDisplayHelper.PrintCourse(course);
        }

        ConsoleHelper.Pause();
    }

    private void SearchCourse()
    {
        ConsoleHelper.PrintHeader("Search Course");

        string keyword =
            ConsoleHelper.ReadString(Prompts.EnterCourseKeyword);

        List<Course> courses =
            _courseService.SearchCourses(keyword);

        if (!courses.Any())
        {
            ConsoleHelper.PrintInformation("No matching courses found.");
            ConsoleHelper.Pause();
            return;
        }

        CourseDisplayHelper.PrintCourses(courses);

        ConsoleHelper.Pause();
    }

    private void DisplayCoursesByCreditHours()
    {
        ConsoleHelper.PrintHeader("Courses By Credit Hours");

        int creditHours =
            ConsoleHelper.ReadInt(Prompts.EnterCreditHours);

        List<Course> courses =
            _courseService.GetCoursesByCreditHours(creditHours);

        if (!courses.Any())
        {
            ConsoleHelper.PrintInformation("No courses found.");
            ConsoleHelper.Pause();
            return;
        }

        CourseDisplayHelper.PrintCourses(courses);

        ConsoleHelper.Pause();
    }

    private void DisplayCoursesOrderedByName()
    {
        ConsoleHelper.PrintHeader("Courses Ordered By Name");

        List<Course> courses = _courseService.GetCoursesOrderedByName();

        if (!courses.Any())
        {
            ConsoleHelper.PrintInformation("No courses found.");
            ConsoleHelper.Pause();
            return;
        }

        CourseDisplayHelper.PrintCourses(courses);

        ConsoleHelper.Pause();
    }

    private void DisplayCoursesOrderedByCreditHours()
    {
        ConsoleHelper.PrintHeader("Courses Ordered By Credit Hours");

        List<Course> courses = _courseService.GetCoursesOrderedByCreditHours();

        if (!courses.Any())
        {
            ConsoleHelper.PrintInformation("No courses found.");
            ConsoleHelper.Pause();
            return;
        }

        CourseDisplayHelper.PrintCourses(courses);

        ConsoleHelper.Pause();
    }
}