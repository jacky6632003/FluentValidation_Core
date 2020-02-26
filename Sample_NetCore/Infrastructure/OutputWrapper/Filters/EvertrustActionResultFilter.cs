using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sample_NetCore.Infrastructure.OutputWrapper.Models;

namespace Sample_NetCore.Infrastructure.OutputWrapper.Filters
{
    /// <summary>
    /// class EvertrustActionResultFilter.
    /// </summary>
    /// <seealso cref="T:Microsoft.AspNetCore.Mvc.Filters.IAsyncActionFilter" />
    public class EvertrustActionResultFilter : IAsyncActionFilter
    {
        /// <inheritdoc />
        /// <summary>
        /// Called asynchronously before the action, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        /// <param name="next">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate" />. Invoked to execute the next action filter or the action itself.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that on completion indicates the filter has executed.
        /// </returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.Result is ObjectResult result)
            {
                switch (result.Value)
                {
                    case HttpResponseMessage _:
                        return;

                    case SuccessResultOutputModel<object> _:
                        return;

                    case FailureResultOutputModel _:
                        return;
                }

                var controllerTypeName = context.Controller.GetType().Name;

                if (controllerTypeName.Equals("ActionController", StringComparison.OrdinalIgnoreCase).Equals(false))
                {
                    var httpMethod = context.HttpContext.Request.Method;

                    if (result.StatusCode >= 400)
                    {
                        var failureResponse = new FailureResultOutputModel
                        {
                            Id = EvertrustAsyncContext.CorrelationId,
                            ApiVersion = EvertrustAsyncContext.Version,
                            Method = $"{context.HttpContext.Request.Path}.{httpMethod}",
                            Status = "Faliure",
                            Errors = new List<FailureInformation> { (FailureInformation)result.Value }
                        };

                        executedContext.Result = new ObjectResult(failureResponse)
                        {
                            StatusCode = result.StatusCode
                        };
                    }
                    else
                    {
                        var successResponse = new SuccessResultOutputModel<object>
                        {
                            Id = EvertrustAsyncContext.CorrelationId,
                            ApiVersion = EvertrustAsyncContext.Version,
                            Method = $"{context.HttpContext.Request.Path}.{httpMethod}",
                            Status = "Success",
                            Data = result.Value
                        };

                        executedContext.Result = new ObjectResult(successResponse)
                        {
                            StatusCode = result.StatusCode
                        };
                    }
                }
            }
        }
    }
}