using ASP.Infracstructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : Controller
    {
        public IPackageService _packageService;
        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var package = _packageService.GetAll();
            return Ok(package);
        }
    }
}
