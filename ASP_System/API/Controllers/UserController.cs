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

        [Authorize]
        [HttpGet("GetValue")]
        public async Task<IActionResult> GetValue()
        {
            return Ok("Oke");
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
            var refreshToken = _jwtTokenService.CreateRefeshToken();
            user.RefreshToken = refreshToken;
            user.DateExpireRefreshToken = DateTime.Now.AddDays(7);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(new { token = accessToken, refreshToken });
        }

        [Authorize]
        [HttpDelete("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            var userName = HttpContext.User.Identity?.Name;
            if (userName is null)
                return Unauthorized();
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                return Unauthorized();
            user.RefreshToken = null;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<IActionResult> getCurrentUserId()
        {
            var user = _currentUserSerivice.GetUserId();
            return Ok(new { userId = user });
        }
        [HttpGet("getAllUser")]
        public Task<IEnumerable<UserDTO>> getAllUsers()
        {
            var listUser = _userServices.GetAllUsers();
            return listUser;
        }
        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> refeshToken(string refreshToken)
        {
            var userId = _currentUserSerivice.GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || !user.IsActive || user.RefreshToken != refreshToken || user.DateExpireRefreshToken < DateTime.UtcNow)
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

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetAllUser(string id)
        {
            var result = await _userServices.GetUserByIDlAsync(id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var result = await _currentUserSerivice.User();
            return Ok(result);
        }
    }
}