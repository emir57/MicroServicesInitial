using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace FreeCourse.Shared.Service
{
    public sealed class SharedIdentityService : ISharedIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId
            => _httpContextAccessor.HttpContext.User
                .FindFirst(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
    }
}
