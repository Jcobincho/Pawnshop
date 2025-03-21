using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
