using Pawnshop.Domain.AuthTokens;
using Pawnshop.Domain.Entities;
using System.Security.Claims;

namespace Pawnshop.Application.JsonWebTokenApplication.Interfaces
{
    public interface IJsonWebTokenService
    {
        JsonWebToken GenerateJsonWebToken(Users user, ICollection<string> roles, ICollection<Claim> claims);
        RefreshToken GenerateRefreshToken();
        void DeleteExpiresRefreshToken(Users user);
    }
}
