using ASP.NetFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetFundamentals.Controllers
{
    public class PracticeViewController : Controller
    {
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
    }
}
