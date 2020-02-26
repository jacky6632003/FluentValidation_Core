using System;
using Sample_NetCore.Infrastructure.OutputWrapper.Models;

namespace Sample_NetCore.Infrastructure.OutputWrapper.Exceptions
{
    /// <summary>
    /// Class ValidateException.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ValidateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateException"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public ValidateException(RequestValidateResult result)
        {
            this.ProgramCode = string.Empty;
            this.Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateException"/> class.
        /// </summary>
        /// <param name="programCode">The program code.</param>
        /// <param name="result">The result.</param>
        public ValidateException(string programCode, RequestValidateResult result)
        {
            this.ProgramCode = programCode;
            this.Result = result;
        }

        /// <summary>
        /// The program code.
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// The result.
        /// </summary>
        public RequestValidateResult Result { get; set; }
    }
}