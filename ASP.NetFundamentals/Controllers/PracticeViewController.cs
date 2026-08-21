using ASP.NetFundamentals.Configurations;
using ASP.NetFundamentals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASP.NetFundamentals.Controllers
{
    public class PracticeViewController : Controller
    {
        private readonly SecretKey _secretKey;
        private readonly ILogger<PracticeViewController> _logger;

        public PracticeViewController(IOptions<SecretKey> secretKey, ILogger<PracticeViewController> logger)
        {
            _secretKey = secretKey.Value;
            _logger = logger;
        }
        public IActionResult RenderIndex()
        {
            return View();
        }

        public IActionResult RazorSyntax()
        {
            var Name = "Zain";
            var Age = 24;
            var Role = "ASP.NET Core Developer";
            var Date = "11 August 2026";

            ViewData["Name"] = Name;
            ViewData["Age"] = Age;
            ViewData["Role"] = Role;
            ViewData["Date"] = Date;
            ViewData["SuccessMessage"] = "Personal Details Printed successfully.";

            return View();
        }

        public IActionResult RenderViewBag()
        {
            ViewBag.Name = "Ghumman Saab";
            ViewBag.Age = 25;
            ViewBag.Designation = "Dotnet Developer";
            ViewBag.Height = "5 feet 7 Inches";
            ViewBag.Email = "zanair.engineer@gmail.com";
            ViewBag.SuccessMessage = "Personal Details Printed successfully.";


            return View();
        }

        public IActionResult RenderTempData()
        {
            TempData["SuccessMessage"] = "Personal Details Printed successfully.";

            return View();
        }

        public IActionResult LogSecretKey()
        {
            var token = _secretKey.AppToken;

            if (string.IsNullOrWhiteSpace(token))
            {
                return Content("Secret key is not configured.");
            }

            return Content("Secret key logged successfully.");
        }
    }
}
