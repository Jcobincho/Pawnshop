using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.Base
{
    public abstract class BaseQuery<TResponse> : BaseQuery, IRequest<TResponse>
        where TResponse : class
    { 
        
    }

    public abstract class BaseQuery
    {
        // dodać sprawdzanie uprawnien
    }
}
