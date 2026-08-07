using StudentManagementSystem.Helpers;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;
using StudentManagementSystem.Strings;

namespace StudentManagementSystem.MenuOptions;

public class DepartmentMenu
{
    private readonly IDepartmentService _departmentService;

    public DepartmentMenu(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public void ShowMenu()
    {
        bool isRunning = true;

        while (isRunning)
        {
            DepartmentDisplayHelper.ShowDepartmentOptions();

            int choice = ConsoleHelper.ReadInt(Prompts.EnterChoice);

            switch (choice)
            {
                case 1:
                    DisplayAllDepartments();
                    break;

                case 2:
                    GetDepartmentById();
                    break;

                case 3:
                    GetDepartmentByCode();
                    break;

                case 4:
                    SearchDepartment();
                    break;

                case 5:
                    DisplayDepartmentsOrderedByName();
                    break;

                case 6:
                    DisplayDepartmentsOrderedByCode();
                    break;

                case 7:
                    DisplayFirstDepartment();
                    break;

                case 8:
                    DisplayLastDepartment();
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

    private void DisplayAllDepartments()
    {
        ConsoleHelper.PrintHeader("All Departments");

        List<Department> departments =
            _departmentService.GetAllDepartments();

        if (!departments.Any())
        {
            ConsoleHelper.PrintInformation("No departments found.");
            ConsoleHelper.Pause();
            return;
        }

        DepartmentDisplayHelper.PrintDepartments(departments);

        ConsoleHelper.Pause();
    }

    private void GetDepartmentById()
    {
        ConsoleHelper.PrintHeader("Get Department By Id");

        int departmentId =
            ConsoleHelper.ReadInt(Prompts.EnterDepartmentId);

        Department? department =
            _departmentService.GetDepartmentById(departmentId);

        if (department is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.DepartmentNotFound);
        }
        else
        {
            DepartmentDisplayHelper.PrintDepartment(department);
        }

        ConsoleHelper.Pause();
    }

    private void GetDepartmentByCode()
    {
        ConsoleHelper.PrintHeader("Get Department By Code");

        string departmentCode =
            ConsoleHelper.ReadString(Prompts.EnterDepartmentCode);

        Department? department =
            _departmentService.GetDepartmentByCode(departmentCode);

        if (department is null)
        {
            ConsoleHelper.PrintError(ErrorMessages.DepartmentNotFound);
        }
        else
        {
            DepartmentDisplayHelper.PrintDepartment(department);
        }

        ConsoleHelper.Pause();
    }

    private void SearchDepartment()
    {
        ConsoleHelper.PrintHeader("Search Department");

        string keyword =
            ConsoleHelper.ReadString(Prompts.EnterDepartmentKeyword);

        List<Department> departments =
            _departmentService.SearchDepartments(keyword);

        if (!departments.Any())
        {
            ConsoleHelper.PrintInformation("No matching departments found.");
            ConsoleHelper.Pause();
            return;
        }

        DepartmentDisplayHelper.PrintDepartments(departments);

        ConsoleHelper.Pause();
    }

    private void DisplayDepartmentsOrderedByName()
    {
        ConsoleHelper.PrintHeader("Departments Ordered By Name");

        List<Department> departments =
            _departmentService.GetDepartmentsOrderedByName();

        if (!departments.Any())
        {
            ConsoleHelper.PrintInformation("No departments found.");
            ConsoleHelper.Pause();
            return;
        }

        DepartmentDisplayHelper.PrintDepartments(departments);

        ConsoleHelper.Pause();
    }

    private void DisplayDepartmentsOrderedByCode()
    {
        ConsoleHelper.PrintHeader("Departments Ordered By Code");

        List<Department> departments =
            _departmentService.GetDepartmentsOrderedByCode();

        if (!departments.Any())
        {
            ConsoleHelper.PrintInformation("No departments found.");
            ConsoleHelper.Pause();
            return;
        }

        DepartmentDisplayHelper.PrintDepartments(departments);

        ConsoleHelper.Pause();
    }

    private void DisplayFirstDepartment()
    {
        ConsoleHelper.PrintHeader("First Department");

        Department? department =
            _departmentService.GetFirstDepartment();

        if (department is null)
        {
            ConsoleHelper.PrintInformation("No department found.");
        }
        else
        {
            DepartmentDisplayHelper.PrintDepartment(department);
        }

        ConsoleHelper.Pause();
    }

    private void DisplayLastDepartment()
    {
        ConsoleHelper.PrintHeader("Last Department");

        Department? department =
            _departmentService.GetLastDepartment();

        if (department is null)
        {
            ConsoleHelper.PrintInformation("No department found.");
        }
        else
        {
            DepartmentDisplayHelper.PrintDepartment(department);
        }

        ConsoleHelper.Pause();
    }
}