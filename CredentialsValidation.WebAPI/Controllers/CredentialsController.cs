using CredentialsValidation.Abstractions;
using CredentialsValidation.Database.Abstractions;
using CredentialsValidation.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace CredentialsValidation.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private IRepository _repository;

        public CredentialsController(IRepository repository, IStringLocalizer<Resource> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseEnvelope>> ValidateCredentials(Credentials credentials)
        {
            ResponseEnvelope responseEnvelope = new ResponseEnvelope
            {
                Success = true,
                Errors = new ErrorCollection()
            };

            try
            {
                // First of all, validate the incoming values
                IValidator validator = new Validator();
              
                // Check if EMail is valid
                if (!validator.ValidateEmail(credentials.EMail))
                {
                    // Email is not valid, set the flag and add the corresponding error to the collection
                    responseEnvelope.Success = false;
                    responseEnvelope.Errors.Add(
                        new Error()
                        {
                            // Validation failed, prepare the error object stating details
                            Type = ErrorType.ValidationError,
                            HumanReadableMessage = _localizer["EMailInvalid"],
                            TechnicalMessage = _localizer["EMailInvalidTechnical"],
                            AdditionalInfo = string.Empty
                        });
                }

                // Check if Password is valid
                if (!validator.ValidatePassword(credentials.Password))
                {
                    // Password is not valid, set the flag and add the corresponding error to the collection
                    responseEnvelope.Success = false;
                    responseEnvelope.Errors.Add(
                       new Error()
                       {
                           // Validation failed, prepare the error object stating details
                           Type = ErrorType.ValidationError,
                           HumanReadableMessage = _localizer["PasswordInvalid"],
                           TechnicalMessage = _localizer["PasswordInvalidTechnical"],
                           AdditionalInfo = string.Empty
                       });
                }

                // Input validation process done, proceed accordingly
                if (responseEnvelope.Success)
                {
                    // Input is correct, check if it already exists
                    if (_repository.CredentialsExists(credentials))
                    {
                        responseEnvelope.Success = false;
                        responseEnvelope.Errors.Add(new Error()
                        {
                            Type = ErrorType.RedundancyError,
                            HumanReadableMessage = _localizer["EMailAlreadyRegistered"]
                        });
                    }
                    else
                    {
                        // New credential, add it to the existing collection/database
                        if (_repository.AddCredentials(credentials))
                        {
                            responseEnvelope.Success = true;
                            responseEnvelope.SuccessMessage = _localizer["CredentialsSuccess"];
                        }
                        else
                        {
                            responseEnvelope.Success = false;
                            responseEnvelope.Errors.Add(new Error()
                            {
                                Type = ErrorType.Exception,
                                HumanReadableMessage = _localizer["ErrorWhileAdding"]
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
                        Type = ErrorType.Exception,
                        HumanReadableMessage = _localizer["UnexpectedException"],
                        TechnicalMessage = ex.Message
                    }
                };
            }

            return responseEnvelope;
        }
    }
}
