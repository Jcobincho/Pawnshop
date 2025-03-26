using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pawnshop.Application.UserClaimsDataProviderApplication.Interfaces;

namespace Pawnshop.Application.Base
{
    public abstract class BaseCommand<TResponse> : BaseCommand, IRequest<TResponse>
        where TResponse : class
    {
    }

    public abstract class BaseCommand
    {
        private static readonly Lazy<IUserClaimsDataProviderService> _userClaimsDataProviderService =
            new Lazy<IUserClaimsDataProviderService>(InitializeService);

        public Guid UserIdFromClaims { get; }

        protected BaseCommand()
        {
            UserIdFromClaims = _userClaimsDataProviderService.Value.GetUserIdFromClaims();
        }

        private static IUserClaimsDataProviderService InitializeService()
        {
            return ServiceRegistration.ServiceProvider?
                .GetRequiredService<IUserClaimsDataProviderService>()
                ?? throw new InvalidOperationException("ServiceProvider not initialized.");
        }
    }

    public static class ServiceRegistration
    {
        public static IServiceProvider? ServiceProvider { get; set; }
    }
}
