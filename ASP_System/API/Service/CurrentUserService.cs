using Domain.Entities;
using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace API.Service
{
    public interface ICurrentUserService
    {
        Guid GetUserId();
        String getUserEmail();
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IActionContextAccessor _actionContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _actionContextAccessor = actionContextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            throw new NotImplementedException();
        }
        public Guid GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            //if(httpContext == null)
            //{
            //    httpContext = _actionContextAccessor.ActionContext?.HttpContext;
            //}
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value);
            //var result =  _userManager.GetUserId(HttpContext.User);
        }
        public String getUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
