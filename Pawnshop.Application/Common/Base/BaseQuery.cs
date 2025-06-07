using MediatR;
using System.Text.Json.Serialization;

namespace Pawnshop.Application.Common.Base
{
    public abstract class BaseQuery<TResponse> : BaseQuery, IRequest<TResponse>
        where TResponse : class
    {

    }

    public abstract class BaseQuery
    {
        [JsonIgnore]
        public Guid UserIdFromClaims { get; internal set; }
    }
}
