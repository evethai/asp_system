using API.Service;
using Application.Interfaces.Services;
using Domain.Model;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosterController : ControllerBase
    {
        private readonly IPosterService _posterService;
        private readonly ICurrentUserService _currentUserService;

        public PosterController(IPosterService posterService, ICurrentUserService currentUserService)
        {
            _posterService = posterService;
            _currentUserService = currentUserService;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _posterService.GetAllPoster();
            return Ok(result);
        }
        [HttpPost("AddPoster")]
        public async Task<IActionResult> AddPoster(PosterAddDTO post)
        {
            try
            {
                var userId = _currentUserService.GetUserId();
                var result = await _posterService.AddPoster(post,userId.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var result = await _posterService.GetPosterById(id);
            return Ok(result);
        }
        [HttpPut("DecreasePost")]
        public async Task<IActionResult> DecreasePost(int id) // Khi artist post bài 
        {

            try
            {
                var userId = _currentUserService.GetUserId();
                var result = await _posterService.DecreasePost(id,userId.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //[HttpPut("Update")]
        //public async Task<IActionResult> UpdatePost(int id, PosterDTO post)
        //{
        //    try
        //    {
        //        var result = await _posterService.UpdatePost(id, post);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
        [HttpPut("QuantityExtensionPost")]
        public async Task<IActionResult> QuantityExtensionPost(int PackageId, int PostId)
        {
            try
            {
                var userId = _currentUserService.GetUserId();
                var result = await _posterService.QuantityExtensionPost(PackageId, PostId, userId.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
