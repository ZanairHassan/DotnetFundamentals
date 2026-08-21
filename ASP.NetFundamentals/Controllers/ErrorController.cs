using ASP.NetFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetFundamentals.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            var model = new ErrorModel
            {
                RequestId = HttpContext.TraceIdentifier,
                StatusCode = Response.StatusCode == 200 ? StatusCodes.Status500InternalServerError : Response.StatusCode
            };

            return View(model);
        }
    }
}