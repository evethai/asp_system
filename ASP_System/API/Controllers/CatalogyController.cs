using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogyController : ControllerBase
    {
        private readonly ICatalogyService _catalogyService;

        public CatalogyController(ICatalogyService catalogyService)
        {
            _catalogyService = catalogyService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _catalogyService.GetAllCatalogy();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult CreateCatalogy(Category catalogy)
        {
            _catalogyService.AddNewCatalogy(catalogy);
            
            return Ok();
        }
    }
}
