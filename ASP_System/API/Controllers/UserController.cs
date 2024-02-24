using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Model;
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
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserServices userServices, UserManager<ApplicationUser> userManager)
        {
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
            
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            
            }
            //var user =  _userManager.GetUserAsync(HttpContext.User);
            //var userId = user.Id;

            //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            return Ok(result);
        }
    }
}
