using API.Service;
using Application.Interfaces.Services;
using Domain.Model;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ICurrentUserService _currentUserService;

        public NotificationController(INotificationService notiService, ICurrentUserService currentUserService)
        {
            _notificationService = notiService;
            _currentUserService = currentUserService;
        }


        //get all
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _notificationService.GetAllNotification();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _notificationService.GetNotificationById(id);
            return Ok(result);
        }

        [HttpPost("CreateNotification")]
        public async Task<IActionResult> CreateNotification([FromForm] CreateNotificationDTO noti)
        {
            try
            {
                var userid = _currentUserService.GetUserId();
                var result = await _notificationService.CreateNotification(noti, userid.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("RemoveNotification/{id}")]
        public async Task<IActionResult> RemoveNotification(int id)
        {
            try
            {
                var result = await _notificationService.RemoveNotification(id);
                if (!result.IsSuccess)
                {
                    return NotFound(); // 404 Not Found if the notification with the given id is not found
                }
                return NoContent(); // 204 No Content indicates successful removal
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
