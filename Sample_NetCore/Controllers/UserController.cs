using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample_NetCore.Infrastructure.ActionFilters;
using Sample_NetCore.Infrastructure.Validators;
using Sample_NetCore.Models.InputParameters;
using Sample_NetCore.Models.OutputModels;

namespace Sample_NetCore.Controlledrs
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost("Signup")]
        [Produces("application/json")]
        [CustomValidator(typeof(UserSignupParameterValidator))]
        public async Task<IActionResult> SignUp([FromBody] UserSignupParameter parameter)
        {
            var output = new UserSignupOutputModel();

            output.Success = true;
            output.Result = "SignUp 完成";

            return this.Ok(output);
        }
    }
}