using Microsoft.AspNetCore.Mvc;
using MiniAssignment.Interfaces;
using MiniAssignment.ViewModels;

namespace MiniAssignment.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IUserService userService, ILogger<DashboardController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var users = _userService.GetAll();

                var viewModel = new DashboardVM
                {
                    TotalUsers = users.Count(),
                    ActiveUsers = users.Count(user => user.IsActive),
                    InactiveUsers = users.Count(user => !user.IsActive)
                };

                _logger.LogInformation("Dashboard loaded. TotalUsers: {TotalUsers}, ActiveUsers: {ActiveUsers}, InactiveUsers: {InactiveUsers}",
                    viewModel.TotalUsers,
                    viewModel.ActiveUsers,
                    viewModel.InactiveUsers);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while loading the dashboard.");

                TempData["ErrorMessage"] = "An unexpected error occurred while loading the dashboard.";

                return RedirectToAction(nameof(UserController.Index), "User");
            }
        }
    }
}