using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sample_NetCore.Infrastructure.OutputWrapper.Exceptions;
using Sample_NetCore.Infrastructure.OutputWrapper.Models;

namespace Sample_NetCore.Infrastructure.OutputWrapper.Filters
{
    /// <summary>
    /// Class ValidateExceptionFilter.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncExceptionFilter" />
    public class ValidateExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (!(context.Exception is ValidateException exception))
            {
                return Task.CompletedTask;
            }

            context.Exception = null;

            var output = new FailureResultOutputModel
            {
                Id = EvertrustAsyncContext.CorrelationId,
                Method = $"{context.HttpContext.Request.Path}.{context.HttpContext.Request.Method}",
                Status = "ValidationError",
                ApiVersion = EvertrustAsyncContext.Version
            };

            output.Errors.Add(new FailureInformation
            {
                ErrorCode = 30001,
                Message = exception.Result.Error.Message,
                Description = exception.Result.Error.Description
            });

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(output);

            return Task.CompletedTask;
        }
    }
}