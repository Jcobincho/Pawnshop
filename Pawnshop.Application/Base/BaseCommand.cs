using MediatR;

namespace Pawnshop.Application.Base
{
    public abstract class BaseCommand<TResponse> : BaseCommand, IRequest<TResponse>
        where TResponse : class
    { 
    
    }

    public abstract class BaseCommand
    {
        // Add user id from http context accessor

        // Dodać sprawdzanie uprawnien
    }
}
