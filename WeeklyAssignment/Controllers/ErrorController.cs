using Microsoft.AspNetCore.Mvc;

namespace WeeklyAssignment.Controllers;

[Route("error")]
public class ErrorController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;

        return View();
    }
}