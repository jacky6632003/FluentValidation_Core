using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sample_NetCore.Infrastructure.OutputWrapper;
using Sample_NetCore.Infrastructure.OutputWrapper.Models;

namespace Sample_NetCore.Infrastructure.ActionFilters
{
    public class CustomValidatorAttribute : ActionFilterAttribute
    {
        private readonly Type _validatorType;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">Type of the validator.</param>
        public CustomValidatorAttribute(Type validatorType)
        {
            this._validatorType = validatorType;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var parameters = context.ActionArguments;
            if (parameters.Count <= 0)
            {
                await base.OnActionExecutionAsync(context, next);
            }

            var parameter = parameters.FirstOrDefault();
            if (parameter.Value == null)
            {
                context.Result = new BadRequestObjectResult("未輸入 Parameter");
            }

            var validator = Activator.CreateInstance(this._validatorType) as IValidator;
            var validationResult = await validator.ValidateAsync(parameter.Value);

            if (validationResult.IsValid.Equals(false))
            {
                var error = validationResult.Errors.FirstOrDefault();

                var failureOutputModel = new FailureResultOutputModel
                {
                    Id = EvertrustAsyncContext.CorrelationId,
                    ApiVersion = EvertrustAsyncContext.Version,
                    Method = $"{context.HttpContext.Request.Path}.{context.HttpContext.Request.Method}",
                    Status = "VaildationError",
                    Errors = new List<FailureInformation>
                    {
                        new FailureInformation
                        {
                            ErrorCode = 30001,
                            Message = "輸入資料驗證錯誤",
                            Description = error.ErrorMessage
                        }
                    }
                };

                context.Result = new BadRequestObjectResult(failureOutputModel);
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}