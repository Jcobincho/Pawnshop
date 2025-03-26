using MediatR;

namespace Pawnshop.Application.Base
{
    public abstract class BaseQuery<TResponse> : BaseQuery, IRequest<TResponse>
        where TResponse : class
    { 
        
    }

    public abstract class BaseQuery
    {
        // Add user id from http context accessor

        // dodać sprawdzanie uprawnien
    }
}
