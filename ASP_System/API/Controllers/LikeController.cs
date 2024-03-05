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
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly ICurrentUserService _currentUserService;

        public LikeController(ILikeService likeService, ICurrentUserService currentUserService)
        {
            _likeService = likeService;
            _currentUserService = currentUserService;
        }

        [Authorize]
        [HttpPost("CreateLike")]
        public async Task<IActionResult> CreateLike([FromForm] LikeCreateDTO like)
        {
            try
            {
                var currentUser = _currentUserService.GetUserId();
                var result = await _likeService.CreateLike(like, currentUser.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("DeleteLike")]
        public async Task<IActionResult> DeleteLike(LikeDeleteDTO like)
        {
            var reponse = await _likeService.DeleteLike(like);
            if (reponse.IsSuccess)
            {
                return Ok(reponse);
            }
            return BadRequest(reponse);
        }

        [HttpGet("GetLike{id}")]
        public async Task<IActionResult> GetLike(int id)
        {
            var result = await _likeService.GetLike(id);
            return Ok(result);
        }

        [HttpGet("GetAllLike")]
        public async Task<IActionResult> GetAllLike()
        {
            var result = await _likeService.GetAllLike();
            return Ok(result);
        }
    }
}
