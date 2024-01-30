using Microsoft.AspNetCore.Mvc;

namespace Asp_System.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
