using Microsoft.AspNetCore.Mvc;
using MiniAssignment.Interfaces;
using MiniAssignment.Models;
using MiniAssignment.ViewModels;

namespace MiniAssignment.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var users = _userService.GetAll();

            var viewModel = new UserListVM
            {
                Users = users,
                TotalUsers = users.Count()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id:int}")]
        public IActionResult Details(int id)
        {
            var user = _userService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            var viewModel = new UserDetailsVM
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    DateOfBirth = model.DateOfBirth!.Value,
                    IsActive = model.IsActive,
                    CreatedAt = DateTime.Now
                };

                _userService.Add(user);

                _logger.LogInformation("User {ID} was created successfully.", user.ID);

                TempData["SuccessMessage"] = $"User {user.FirstName} {user.LastName} was created successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Unable to create user with email {Email}.", model.Email);

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }   
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating user.");

                ModelState.AddModelError(string.Empty, "An unexpected error occurred while creating the user.");

                return View(model);
            }
        }

        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            var viewModel = new EditUserVM
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive
            };

            return View(viewModel);
        }

        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditUserVM model)
        {
            if (id != model.ID)
            {
                ModelState.AddModelError(string.Empty, "The requested user does not match the submitted user.");

                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var existingUser = _userService.GetById(id);

                if (existingUser is null)
                {
                    return NotFound();
                }

                existingUser.FirstName = model.FirstName;
                existingUser.LastName = model.LastName;
                existingUser.Email = model.Email;
                existingUser.PhoneNumber = model.PhoneNumber;
                existingUser.DateOfBirth = model.DateOfBirth!.Value;
                existingUser.IsActive = model.IsActive;

                _userService.Update(existingUser);

                _logger.LogInformation("User {ID} was updated successfully.", id);

                TempData["SuccessMessage"] = $"User {existingUser.FirstName} {existingUser.LastName} was updated successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Unable to update user {ID}.", id);

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating user {ID}.", id);

                ModelState.AddModelError(string.Empty, "An unexpected error occurred while updating the user.");

                return View(model);
            }
        }

        [HttpGet("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);

            if (user is null)
            {
                return NotFound();
            }

            var viewModel = new DeleteUserVM
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsActive = user.IsActive
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, DeleteUserVM model)
        {
            if (id != model.ID)
            {
                ModelState.AddModelError(string.Empty, "The requested user does not match the submitted user.");

                return View(model);
            }

            try
            {
                var user = _userService.GetById(id);

                if (user is null)
                {
                    return NotFound();
                }

                _userService.Delete(id);

                _logger.LogInformation("User {Id} was deleted successfully.", id);

                TempData["SuccessMessage"] = $"User {user.FirstName} {user.LastName} was deleted successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Unable to delete user {Id}.", id);

                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting user {Id}.", id);

                ModelState.AddModelError(string.Empty, "An unexpected error occurred while deleting the user.");

                return View(model);
            }
        }

        [HttpGet("users/active")]
        public IActionResult ActiveUsers()
        {
            try
            {
                var users = _userService.GetActiveUsers();

                var viewModel = new UserListVM
                {
                    Users = users,
                    TotalUsers = users.Count()
                };

                _logger.LogInformation("Active users list loaded. Count: {Count}", viewModel.TotalUsers);

                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading active users.");

                TempData["ErrorMessage"] = "An error occurred while loading active users.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("users/inactive")]
        public IActionResult InactiveUsers()
        {
            try
            {
                var users = _userService.GetInactiveUsers();

                var viewModel = new UserListVM
                {
                    Users = users,
                    TotalUsers = users.Count()
                };

                _logger.LogInformation("Inactive users list loaded. Count: {Count}", viewModel.TotalUsers);

                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while loading inactive users.");

                TempData["ErrorMessage"] = "An error occurred while loading inactive users.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}