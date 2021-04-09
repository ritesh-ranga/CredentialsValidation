using CredentialsValidation.Abstractions;
using CredentialsValidation.Database.Abstractions;
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
        private IRepository Repository;
        public CredentialsController(IRepository repository)
        {
            Repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseEnvelope>> ValidateCredentials(Credentials credentials)
        {
            ResponseEnvelope responseEnvelope = new ResponseEnvelope();

            try
            {
                // First of all, validate the incoming values
                IValidator validator = new Validator();
                responseEnvelope = (ResponseEnvelope)validator.Validate(credentials);

                if (responseEnvelope.Success)
                {
                    // Input is correct, check if it already exists
                    if (Repository.CredentialsExists(credentials))
                    {
                        responseEnvelope.Success = false;
                        responseEnvelope.Errors.Add(new Error()
                        {
                            Type = ErrorType.RedundancyError,
                            HumanReadableMessage = "The provided E-Mail address is already registered!"
                        });
                    }
                    else
                    {
                        // Add it to the existing collection/database
                        if (Repository.AddCredentials(credentials))
                        {
                            responseEnvelope.Success = true;
                            responseEnvelope.SuccessMessage = "Credentials validated and registered successfully!";
                        }
                        else
                        {
                            responseEnvelope.Success = false;
                            responseEnvelope.Errors.Add(new Error()
                            {
                                Type = ErrorType.Exception,
                                HumanReadableMessage = "The provided E-Mail address cannot be successfully registered!"
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the exception

                responseEnvelope.Success = false;
                responseEnvelope.Errors = new ErrorCollection
                {
                    new Error()
                    {
                        Type= ErrorType.Exception,
                        HumanReadableMessage="An unexpected error occured during registration!",
                        TechnicalMessage=ex.Message
                    }
                };
            }

            return responseEnvelope;
        }
    }
}
