using MediatR;
using System.Text.Json.Serialization;

namespace Pawnshop.Application.Base
{
    public abstract class BaseCommand<TResponse> : BaseCommand, IRequest<TResponse>
        where TResponse : class
    {
    }

    public abstract class BaseCommand
    {
        [JsonIgnore]
        public Guid UserIdFromClaims { get; internal set; }
    }
}