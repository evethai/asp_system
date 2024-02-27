using API.Service;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserServices userServices,ICurrentUserService userService, UserManager<ApplicationUser> userManager)
        {
            _currentUserService = userService;
            _userServices = userServices;
            _userManager = userManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserSignUpDTO signUpModel)
        {
            var result = await _userServices.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserSignInDTO signInModel)
        {
            var result = await _userServices.SignInAsync(signInModel);

            var user = _userManager.GetUserAsync(User);
            var userId = user.Id;

            if (string.IsNullOrEmpty(result))
            {
                
                return Unauthorized();
            }

            return Ok(new {token = result, id = userId});
        }
    }
}
