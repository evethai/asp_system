using System.Security.Claims;

namespace API.Service
{
    public interface ICurrentUserService
    {
        Guid GetCurrentUserId();
    }
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
