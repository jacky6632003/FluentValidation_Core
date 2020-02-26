using System;

namespace Sample_NetCore.Models.OutputModels
{
    /// <summary>
    /// class UserSignupOutputModel
    /// </summary>
    public class UserSignupOutputModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSignupOutputModel"/> class.
        /// </summary>
        public UserSignupOutputModel()
        {
            this.Success = false;
            this.Result = string.Empty;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserSignupOutputModel"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// The result.
        /// </summary>
        public string Result { get; set; }
    }
}