using Microsoft.AspNetCore.Http;
using Pawnshop.Application.UserClaimsDataProviderApplication.Interfaces;
using System.Security.Claims;

namespace Pawnshop.Infrastructure.Services.UserClaimsDataProvidesInfrastructure.Services
{
    internal class UserClaimsDataProviderService : IUserClaimsDataProviderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClaimsDataProviderService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserIdFromClaims()
        {
            var claims = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(claims)) return Guid.Empty;

            var parseToGuid = Guid.TryParse(claims, out var userId);

            if(!parseToGuid || userId == Guid.Empty) return Guid.Empty;

            return userId;
        }

        public string GetUserNameFromClaims()
        {
            var claims = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrWhiteSpace(claims)) return string.Empty;

            return claims.ToString();
        }
    }
}
