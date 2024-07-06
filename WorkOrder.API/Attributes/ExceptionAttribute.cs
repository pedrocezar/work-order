using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Enums;
using WorkOrder.Domain.Exceptions;
using WorkOrder.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WorkOrder.API.Attributes
{
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var response = new InformationResponse();

            if (context.Exception is InformationException informacaoException)
            {
                response.Status = informacaoException.Status;
                response.Messages = informacaoException.Messages;
                response.Details = $"{context.Exception.Message} | {context.Exception.InnerException?.Message}";
            }
            else
            {
                response.Status = StatusException.Error;
                response.Messages = new List<string> { "Erro inesperdado" };
                response.Details = $"{context.Exception?.Message} | {context.Exception?.InnerException?.Message}";
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.Status.GetStatusCode()
            };

            OnException(context);
            return Task.CompletedTask;
        }
    }
}
