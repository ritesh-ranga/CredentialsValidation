using CredentialsValidation.Abstractions;
using System;
using System.Text.RegularExpressions;

namespace CredentialsValidation.Shared
{
    /// <summary>
    /// Class that exposes validation methods for various fields
    /// </summary>
    public class Validator : IValidator
    {
        /// <summary>
        /// A method that takes <see cref="Credentials"/> object and validates the fields contained within
        /// </summary>
        /// <param name="CredentialsToBeValidated">A <see cref="Credentials"/> object to be validated</param>
        /// <returns>A <see cref="IResponseEnvelope"/> that contains the validation result along with errors, if any</returns>
        public IResponseEnvelope Validate(ICredentials CredentialsToBeValidated)
        {
            IResponseEnvelope responseEnvelope = new ResponseEnvelope
            {
                Success = true,
                Errors = new ErrorCollection()
            };

            // Check if EMail is valid
            if (!ValidateEmail(CredentialsToBeValidated.EMail, out Error error))
            {
                // Email is not valid, set the flag and add the corresponding error to the collection
                responseEnvelope.Success = false;
                responseEnvelope.Errors.Add(error);
            }

            // Check if Password is valid
            if (!ValidatePassword(CredentialsToBeValidated.Password, out error))
            {
                // Password is not valid, set the flag and add the corresponding error to the collection
                responseEnvelope.Success = false;
                responseEnvelope.Errors.Add(error);
            }

            // If all the fields of the credential object are validated, set the appropriate message
            if (responseEnvelope.Success)
            {
                responseEnvelope.SuccessMessage = "Credentials validated successfully!";
            }

            return responseEnvelope;
        }

        /// <summary>
        /// A method that validates an Email address by matching it against a regular expression
        /// </summary>
        /// <param name="Email">An Email address to be validated of type <see cref="string"/></param>
        /// <returns>A <see cref="bool"/> flag representing if the validation was success or failure</returns>
        public bool ValidateEmail(string Email, out Error error)
        {
            bool isValid = false;
            error = new Error();

            try
            {
                if (!string.IsNullOrEmpty(Email.Trim()))
                {
                    if (Regex.IsMatch(Email, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
                    {
                        // Validation successful
                        isValid = true;
                    }
                    else
                    {
                        // Validation failed, prepare the error object stating details
                        error.Type = ErrorType.ValidationError;
                        error.HumanReadableMessage = "Provided E-Mail address is not valid.";
                        error.TechnicalMessage = "Regex match failed!";
                        error.AdditionalInfo = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the exception

                error.Type = ErrorType.Exception;
                error.HumanReadableMessage = "An unexpected error occured!";
                error.TechnicalMessage = ex.Message;
                error.AdditionalInfo = ex.StackTrace;
            }

            return isValid;
        }

        /// <summary>
        /// A method to validate Password
        /// </summary>
        /// <param name="Password">A Password to be validated of type <see cref="string"/></param>
        /// <returns>A <see cref="bool"/> flag representing if the validation was success or failure</returns>
        public bool ValidatePassword(string Password, out Error error)
        {
            bool isValid = false;
            error = new Error();

            try
            {
                if (Password.Trim().Length >= 8)
                {
                    // Validation successful
                    isValid = true;
                }
                else
                {
                    // Validation failed, prepare the error object stating details
                    error.Type = ErrorType.ValidationError;
                    error.HumanReadableMessage = "Provided Password is not valid. It should be at least 8 characters long!";
                    error.TechnicalMessage = "The password is less than 8 characters long!";
                    error.AdditionalInfo = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the exception

                error.Type = ErrorType.Exception;
                error.HumanReadableMessage = "An unexpected error occured!";
                error.TechnicalMessage = ex.Message;
                error.AdditionalInfo = ex.StackTrace;
            }

            return isValid;
        }
    }
}
