using ASP.NetFundamentals.Interfaces;
using ASP.NetFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetFundamentals.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var developers = _developerService.GetAll();

            return View(developers);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var developer = _developerService.GetById(id);

            if (developer is null)
            {
                TempData["ErrorMessage"] = "Developer could not be found.";

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Developer Fetched successfully.";

            return View(developer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return View(developer);
            }

            _developerService.Create(developer);

            TempData["SuccessMessage"] = "Developer created successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var developer = _developerService.GetById(id);

            if (developer is null)
            {
                return NotFound();
            }

            return View(developer);
        }

        [HttpPost]
        public IActionResult Edit(int id, Developer developer)
        {
            if (!ModelState.IsValid)
            {
                return View(developer);
            }

            var updated = _developerService.Update(id, developer);

            if (!updated)
            {
                TempData["ErrorMessage"] = "Developer could not be found.";

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Developer updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var developer = _developerService.GetById(id);

            if (developer is null)
            {
                TempData["ErrorMessage"] = "Developer has not found.";

                return RedirectToAction(nameof(Index));
            }

            return View(developer);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleted = _developerService.Delete(id);

            if (!deleted)
            {
                TempData["ErrorMessage"] = "Developer has not been deleted.";

                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Developer deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
