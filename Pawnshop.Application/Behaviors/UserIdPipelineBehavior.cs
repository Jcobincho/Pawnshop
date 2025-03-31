﻿using MediatR;
using Pawnshop.Application.Base;
using Pawnshop.Application.UserClaimsDataProviderApplication.Interfaces;

namespace Pawnshop.Infrastructure.Behaviors
{
    public class UserIdPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : BaseCommand
    {
        private readonly IUserClaimsDataProviderService _userClaimsService;

        public UserIdPipelineBehavior(IUserClaimsDataProviderService userClaimsService)
        {
            _userClaimsService = userClaimsService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            request.UserIdFromClaims = _userClaimsService.GetUserIdFromClaims();
            return await next();
        }
    }
}