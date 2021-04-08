namespace CredentialsValidation.Abstractions
{
    /// <summary>
    /// A class representing an error that occured while validating a <see cref="ICredentials"/> field
    /// </summary>
    public class Error
    {
        public ErrorType Type { get; set; }

        public string TechnicalMessage { get; set; }

        public string HumanReadableMessage { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
