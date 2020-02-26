using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sample_NetCore.Infrastructure.OutputWrapper.Exceptions;
using Sample_NetCore.Infrastructure.OutputWrapper.Models;

namespace Sample_NetCore.Infrastructure.OutputWrapper.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// class EvertrustExceptionFilter.
    /// </summary>
    /// <seealso cref="T:Microsoft.AspNetCore.Mvc.Filters.IAsyncExceptionFilter" />
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        /// <inheritdoc />
        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that on completion indicates the filter has executed.
        /// </returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var isValidateException = context.Exception is ValidateException;

            if (isValidateException.Equals(false))
            {
                var result = new FailureResultOutputModel
                {
                    Id = AsyncContext.CorrelationId,
                    Method = $"{context.HttpContext.Request.Path}.{context.HttpContext.Request.Method}",
                    Status = "ValidationError",
                    ApiVersion = AsyncContext.Version,
                    Errors = new List<FailureInformation>
                    {
                        new FailureInformation
                        {
                            ErrorCode = 30001,
                            Message = context.Exception.Message,
                            Description = context.Exception.ToString()
                        }
                    }
                };

                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                context.ExceptionHandled = true;

                await Task.Yield();
            }
        }
    }
}