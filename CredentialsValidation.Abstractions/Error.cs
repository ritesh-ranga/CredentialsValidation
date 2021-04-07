namespace CredentialsValidation.Abstractions
{
    /// <summary>
    /// A class representing an error
    /// </summary>
    public class Error
    {
        public string Type { get; set; }

        public string TechnicalMessage { get; set; }

        public string HumanReadableMessage { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
