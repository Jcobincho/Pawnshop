using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.Base
{
    public abstract class BaseCommand<TResponse> : BaseCommand, IRequest<TResponse> { }

    public abstract class BaseCommand
    {
        public DateTime CommandExecutedDateTime { get; set; } = DateTime.Now;
        
        // Add user id from http context accessor

        // Dodać sprawdzanie uprawnien
    }
}
