using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IArtworkService _artworkService;
        public TestController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _artworkService.GetAllArtworks();
            return Ok(result);
        }
        //get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _artworkService.GetArtworkById(id);
            return Ok(result);
        }
        //post add
        [HttpPost ("AddArtwork")]
        public async Task<IActionResult> AddArtwork(ArtworkAddDTO artwork)
        {
            try
            {
                var result = await _artworkService.AddArtwork(artwork);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //put update
        [HttpPut("UpdateArtwork")]
        public async Task<IActionResult> UpdateArtwork(ArtworkDTO artwork)
        {
            try
            {
                var result = await _artworkService.UpdateArtwork(artwork);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetArtworkByFilter")]
        public async Task<IActionResult> GetArtworkByFilter(ArtworkFilterParameterDTO filter)
        {
            var result = await _artworkService.GetArtworkByFilter(filter);
            return Ok(result);
        }
    }
}
