using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FreeCourse.Shared.Service
{
    public sealed class SharedIdentityService : ISharedIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var user = _httpContextAccessor.HttpContext.User.Claims;
        }

        public string GetUserId
            => _httpContextAccessor.HttpContext.User
                .FindFirst(JwtRegisteredClaimNames.Sub).Value;
    }
}
