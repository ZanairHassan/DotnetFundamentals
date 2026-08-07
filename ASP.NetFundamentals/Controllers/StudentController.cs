using ASP.NetFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetFundamentals.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            var students = HttpContext.Items["Students"] as IReadOnlyList<Student>;

            return View(students);
        }
    }
}
