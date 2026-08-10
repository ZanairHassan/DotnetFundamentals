using ASP.NetFundamentals.Interfaces;
using ASP.NetFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetFundamentals.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index(string? searchTerm)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                employees = _employeeService.GetAll();
            }
            else
            {
                employees = _employeeService.Search(searchTerm);
            }

            ViewBag.SearchTerm = searchTerm;

            return View(employees);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Employee? employee = _employeeService.GetById(id);

            if (employee is null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            _employeeService.Add(employee);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee? employee = _employeeService.GetById(id);

            if (employee is null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            bool updated = _employeeService.Update(employee);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee? employee = _employeeService.GetById(id);

            if (employee is null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool deleted = _employeeService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult TestException()
        {
            throw new Exception("Testing global exception handling middleware.");
        }
    }
}