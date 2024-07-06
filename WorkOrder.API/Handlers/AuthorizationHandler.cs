using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Enums;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;

namespace WorkOrder.API.Handlers
{
    public class AuthorizationHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly IAuthorizationMiddlewareResultHandler _handler;

        public AuthorizationHandler()
        {
            _handler = new AuthorizationMiddlewareResultHandler();
        }

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            var informacaoResponse = new InformationResponse();

            if (!authorizeResult.Succeeded)
            {
                if (authorizeResult.Forbidden)
                {
                    context.Response.StatusCode = 403;
                    informacaoResponse = new InformationResponse
                    {
                        Status = StatusException.ProhibitedAccess,
                        Messages = new List<string> { "Acesso não permitido" }
                    };
                }
                else
                {
                    context.Response.StatusCode = 401;
                    informacaoResponse = new InformationResponse
                    {
                        Status = StatusException.NotAuthorized,
                        Messages = new List<string> { "Acesso negado" }
                    };
                }

                await context.Response.WriteAsJsonAsync(informacaoResponse);
            }
            else
                await _handler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
