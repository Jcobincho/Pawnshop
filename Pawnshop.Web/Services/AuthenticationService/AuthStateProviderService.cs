using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Pawnshop.Application.UsersApplication.Commands.Logout;
using Pawnshop.Application.UsersApplication.Responses;
using Pawnshop.Domain.AuthTokens;
using Pawnshop.Web.Services.ApiService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Pawnshop.Web.Services.AuthenticationService
{
    public class AuthStateProviderService : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;
        private AuthenticationState _authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public AuthStateProviderService(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return _authenticationState;
        }

        public async Task InitializeAuthenticationState()
        {
            var sessionModel = (await _localStorage.GetAsync<JsonWebToken>("sessionState")).Value;
            var identity = sessionModel == null ? new ClaimsIdentity() : GetClaimsIdentity(sessionModel.AccessToken);
            var user = new ClaimsPrincipal(identity);
            _authenticationState = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(_authenticationState));
        }

        public async Task MarkUserAsAuthenticated(JsonWebToken model)
        {
            await _localStorage.SetAsync("sessionState", model);
            await InitializeAuthenticationState();
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.DeleteAsync("sessionState");
            _authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            NotifyAuthenticationStateChanged(Task.FromResult(_authenticationState));
        }

        private ClaimsIdentity GetClaimsIdentity(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }
    }


}
