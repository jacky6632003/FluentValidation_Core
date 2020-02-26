using System;
using FluentValidation;
using Sample_NetCore.Models.InputParameters;

namespace Sample_NetCore.Infrastructure.Validators
{
    /// <summary>
    /// class UserSignupParameterValidator
    /// </summary>
    /// <seealso cref="UserSignupParameter" />
    public class UserSignupParameterValidator : AbstractValidator<UserSignupParameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignupParameterValidator"/> class.
        /// </summary>
        public UserSignupParameterValidator()
        {
            this.RuleFor(x => x.Account)
                .NotNull().WithMessage("Account 不可為 null")
                .NotEmpty().WithMessage("未輸入 Account")
                .EmailAddress().WithMessage("Account 格式不正確");

            this.RuleFor(x => x.Password)
                .NotNull().WithMessage("Password 不可為 null")
                .NotEmpty().WithMessage("未輸入 Password")
                .MinimumLength(7).WithMessage("Password 長度須大於 6");
        }
    }
}