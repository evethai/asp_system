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
    public class UserNotifcationController : ControllerBase
    {
        private readonly IUserNotificationService _userNotificationService;
        private readonly ICurrentUserService _currentUserService;
        public UserNotifcationController(IUserNotificationService notiService, ICurrentUserService currentUserService)
        {
            _userNotificationService = notiService;
            _currentUserService = currentUserService;
        }
        [Authorize]
        [HttpPost("CreateNotification")]
        public async Task<IActionResult> CreateUserNotification([FromForm] CreateUserNotificationDTO noti)
        {
            try
            {
                var userid = _currentUserService.GetUserId();
                var result = await _userNotificationService.CreateUserNotification(noti, userid.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{userId}/notifications")]
        public async Task<ActionResult<IEnumerable<GetUserNotificationDTO>>> GetNotificationByUserId(string userId)
        {
            var notifications = await _userNotificationService.GetNotificationByUserId(userId);

            if (notifications == null)
            {
                return NotFound(); // or handle as needed
            }

            return Ok(notifications);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUserNotification(int id)
        {
            var response = await _userNotificationService.RemoveUserNotification(id);

            if (response.IsSuccess)
            {
                return Ok(response); // 200 OK
            }

            return BadRequest(response); // 400 Bad Request
        }
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> RemoveAllUserNotificationsByUserId(string userId)
        {
            var response = await _userNotificationService.RemoveAllUserNotificationsByUserId(userId);

            if (response.IsSuccess)
            {
                return Ok(response); // 200 OK
            }

            return BadRequest(response); // 400 Bad Request
        }
    }
}
