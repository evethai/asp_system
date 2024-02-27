using API.Service;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Model;
using Firebase.Auth;
using Infrastructure.Data;
using Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserSerivice;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserServices userServices, IJwtTokenService jwtTokenService, ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            ICurrentUserService currentUserService, RoleManager<IdentityRole> roleManager)
        {
            _currentUserService = userService;
            _userServices = userServices;
            _jwtTokenService = jwtTokenService;
            _context = context;
            _userManager = userManager;
            _currentUserSerivice = currentUserService;
            _roleManager = roleManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserSignUpDTO signUpModel)
        {
            var result = await _userServices.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return StatusCode(500);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserSignInDTO signInModel)
        {
            var user = await _userServices.SignInAsync(signInModel);
            if (user == null)
            {
                
                return Unauthorized();
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenService.CreateToken(user, userRoles);
            var refeshToken = _jwtTokenService.CreateRefeshToken();
            return Ok(new { token = accessToken, refeshToken });
        }
        [Authorize]
        [HttpGet("currentUser")] 
        public async Task<IActionResult> getCurrentUserId()
        {
            var user = _currentUserSerivice.GetUserId();
            return Ok(new { userId = user });
        }
        [Authorize]
        [HttpPost("refesh-token")]
        public async Task<IActionResult> refeshToken(string refeshToken)
        {
            var userId = _currentUserSerivice.GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || !user.IsActive)
            {
                return BadRequest(new { processStatus = ProcessStatus.NotPermission });
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var newRefreshToken = _jwtTokenService.CreateRefeshToken();
            user.RefreshToken = newRefreshToken;
            user.DateExpireRefreshToken = DateTime.Now.AddDays(7);
            var token = _jwtTokenService.CreateToken(user, userRoles);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(new { token = token, refreshToken = newRefreshToken });
        }
    }
}
