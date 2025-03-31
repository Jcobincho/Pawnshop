using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pawnshop.Application.JsonWebTokenApplication.Interfaces;
using Pawnshop.Domain.AuthTokens;
using Pawnshop.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Pawnshop.Infrastructure.Services.JsonWebTokenInfrastructure.Services
{
    internal sealed class JsonWebTokenService : IJsonWebTokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JsonWebTokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public JsonWebToken GenerateJsonWebToken(Users user, ICollection<string> roles, ICollection<Claim> claims)
        {
            DateTime dtNow = DateTime.Now;

            var jwtClaims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(dtNow).ToUnixTimeSeconds().ToString())
            };

            if(roles?.Any() == true)
            {
                foreach(var role in roles)
                {
                    jwtClaims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            if(claims?.Any() == true)
            {
                var customClaims = new List<Claim>();

                foreach(var claim in claims)
                {
                    customClaims.Add(new Claim(claim.Type, claim.Value));
                }
                jwtClaims.AddRange(customClaims);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = dtNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            jwtClaims,
            now,
            expires: expires,
            signingCredentials: cred
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
                UserId = user.Id,
                Roles = roles,
                Claims = claims?.ToDictionary(x => x.Type, x => x.Value)
            };
        }

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpire)
            };
        }

        public void DeleteExpiresRefreshToken(Users user)
        {
            var expiredRefreshToken = user.RefreshToken.Where(token => token.IsExpired).ToList();

            foreach(var token in expiredRefreshToken)
            {
                if(token.IsExpired)
                {
                    user.DeleteRefreshToken(token);
                }
            }
        }
    }
}
