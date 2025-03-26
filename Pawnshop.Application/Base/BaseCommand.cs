using MediatR;

namespace Pawnshop.Application.Base
{
    public abstract class BaseCommand<TResponse> : BaseCommand, IRequest<TResponse>
        where TResponse : class
    {
    }

    public abstract class BaseCommand
    {
        public Guid UserIdFromClaims { get; set; }
    }
}