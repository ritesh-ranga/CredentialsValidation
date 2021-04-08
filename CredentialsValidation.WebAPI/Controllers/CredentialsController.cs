using CredentialsValidation.Abstractions;
using CredentialsValidation.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CredentialsValidation.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ResponseEnvelope>> ValidateCredentials(Credentials credentials)
        {
            ResponseEnvelope responseEnvelope = new ResponseEnvelope();

            try
            {
                IValidator validator = new Validator();
                responseEnvelope = (ResponseEnvelope)validator.Validate(credentials);
            }
            catch (Exception ex)
            {
                // TODO: Log the exception

                responseEnvelope.Success = false;
            }

            return responseEnvelope;
        }
    }
}
