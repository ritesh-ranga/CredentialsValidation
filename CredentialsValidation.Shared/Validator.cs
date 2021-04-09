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
        /// A method that validates an Email address by matching it against a regular expression
        /// </summary>
        /// <param name="Email">An Email address to be validated of type <see cref="string"/></param>
        /// <returns>A <see cref="bool"/> flag representing if the validation was success or failure</returns>
        public bool ValidateEmail(string Email)
        {
            bool isValid = false;

            try
            {
                if (!string.IsNullOrEmpty(Email.Trim()))
                {
                    if (Regex.IsMatch(Email, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
                    {
                        // Validation successful
                        isValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
            }

            return isValid;
        }

        /// <summary>
        /// A method to validate Password
        /// </summary>
        /// <param name="Password">A Password to be validated of type <see cref="string"/></param>
        /// <returns>A <see cref="bool"/> flag representing if the validation was success or failure</returns>
        public bool ValidatePassword(string Password)
        {
            bool isValid = false;

            try
            {
                if (Password.Trim().Length >= 8)
                {
                    // Validation successful
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
            }

            return isValid;
        }
    }
}
