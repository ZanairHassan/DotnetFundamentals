using Microsoft.AspNetCore.Mvc;
using WeeklyAssignment.Filters;
using WeeklyAssignment.Models;
using WeeklyAssignment.Services.Interfaces;
using WeeklyAssignment.ViewModels.Employees;

namespace WeeklyAssignment.Controllers;

[Route("employees")]
[ServiceFilter(typeof(ActionExecutionLoggingFilter))]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("")]
    public IActionResult Index(string? searchTerm, int? designationId)
    {
        var viewModel = new EmployeeListVM
        {
            Employees = _employeeService.Search(searchTerm, designationId),

            Designations = _employeeService.GetDesignations(),

            SearchTerm = searchTerm,

            DesignationId = designationId
        };

        return View(viewModel);
    }

    [HttpGet("{id:int}")]
    public IActionResult Details(int id)
    {
        var employee = _employeeService.GetById(id);

        if (employee is null)
        {
            return NotFound();
        }

        var designation = _employeeService.GetDesignations().FirstOrDefault(d => d.Id == employee.DesignationId);

        var viewModel = new EmployeeDetailsVM
        {
            Employee = employee,
            Designation = designation
        };

        return View(viewModel);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        var viewModel = new EmployeeCreateVM
        {
            Designations = _employeeService.GetDesignations()
        };

        return View(viewModel);
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeCreateVM model)
    {
        if (!ModelState.IsValid)
        {
            model.Designations = _employeeService.GetDesignations();

            return View(model);
        }

        var employee = new Employee
        {
            Name = model.Name,
            Email = model.Email,
            Salary = model.Salary,
            JoiningDate = model.JoiningDate,
            DesignationId = model.DesignationId
        };

        if (!_employeeService.Create(employee))
        {
            ModelState.AddModelError(nameof(model.DesignationId), "The selected designation is invalid.");

            model.Designations = _employeeService.GetDesignations();

            return View(model);
        }

        TempData["SuccessMessage"] = "Employee created successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/edit")]
    public IActionResult Edit(int id)
    {
        var employee = _employeeService.GetById(id);

        if (employee is null)
        {
            return NotFound();
        }

        var viewModel = new EmployeeEditVM
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Salary = employee.Salary,
            JoiningDate = employee.JoiningDate,
            DesignationId = employee.DesignationId,
            Designations = _employeeService.GetDesignations()
        };

        return View(viewModel);
    }

    [HttpPost("{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(
        int id,
        EmployeeEditVM model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            model.Designations = _employeeService.GetDesignations();

            return View(model);
        }

        var employee = new Employee
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            Salary = model.Salary,
            JoiningDate = model.JoiningDate,
            DesignationId = model.DesignationId
        };

        if (!_employeeService.Update(employee))
        {
            if (!_employeeService.Exists(id))
            {
                return NotFound();
            }

            ModelState.AddModelError(nameof(model.DesignationId), "The selected designation is invalid.");

            model.Designations = _employeeService.GetDesignations();

            return View(model);
        }

        TempData["SuccessMessage"] = "Employee updated successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/delete")]
    public IActionResult Delete(int id)
    {
        var employee = _employeeService.GetById(id);

        if (employee is null)
        {
            return NotFound();
        }

        var designation = _employeeService.GetDesignations().FirstOrDefault(d => d.Id == employee.DesignationId);

        var viewModel = new EmployeeDetailsVM
        {
            Employee = employee,
            Designation = designation
        };

        return View(viewModel);
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_employeeService.Delete(id))
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Employee deleted successfully.";

        return RedirectToAction(nameof(Index));
    }
}