using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Models;
using RepositoryPattern.Services.Interfaces;
using RepositoryPattern.ViewModels;

namespace RepositoryPattern.Controllers;

[Route("users")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string? searchTerm)
    {
        IReadOnlyList<User> users = await _userService.GetAllAsync();

        IReadOnlyList<Career> careers = await _userService.GetCareersAsync();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            users = users
                .Where(user =>
                    user.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    user.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    user.Email.Contains(searchTerm,StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        UserListVM viewModel = new()
        {
            Users = users,
            Careers=careers,
            SearchTerm = searchTerm
        };

        return View(viewModel);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        User? user = await _userService.GetByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new UserCreateVM());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        User user = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            DateOfBirth = model.DateOfBirth,
            IsActive = model.IsActive
        };

        User? createdUser = await _userService.CreateAsync(user);

        if (createdUser is null)
        {
            ModelState.AddModelError(nameof(model.Email), "A user with this email address already exists.");

            return View(model);
        }

        TempData["SuccessMessage"] = "User created successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        User? user = await _userService.GetByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        UserEditVM model = new UserEditVM()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            IsActive = user.IsActive
        };

        return View(model);
    }

    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UserEditVM model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        User user = new User()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            DateOfBirth = model.DateOfBirth,
            IsActive = model.IsActive
        };

        User? updatedUser = await _userService.UpdateAsync(user);

        if (updatedUser is null)
        {
            ModelState.AddModelError(nameof(model.Email), "The user could not be updated. The email may already be in use or the user may no longer exist.");

            return View(model);
        }

        TempData["SuccessMessage"] = "User updated successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        User? user = await _userService.GetByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost("delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        bool deleted = await _userService.DeleteAsync(id);

        if (!deleted)
        {
            TempData["ErrorMessage"] = "The user could not be deleted.";

            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "User deleted successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("assigncareer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignCareer(
    UserCareerAssignmentVM model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please select at least one user and a career.";

            return RedirectToAction(nameof(Index));
        }

        bool assigned = await _userService.AssignCareerAsync(model.SelectedUserIds, model.CareerId);

        if (!assigned)
        {
            TempData["ErrorMessage"] = "The career could not be assigned. Please verify that the selected users and career exist.";

            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Career assigned successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("test")]
    public IActionResult TestException()
    {
        throw new Exception("Testing global exception handling Action Filter.");
    }
}