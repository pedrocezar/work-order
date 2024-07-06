using WorkOrder.Domain.Contracts.Responses;
using WorkOrder.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WorkOrder.API.Attributes
{
    public class ValidationAttribute : ActionFilterAttribute
    {
        public override async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();

                var response = new InformationResponse
                {
                    Status = StatusException.IncorrectFormat,
                    Messages = errors
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 400
                };
            }

            OnResultExecuting(context);
            if (!context.Cancel)
            {
                OnResultExecuted(await next());
            }
        }
    }
}
