using CredentialsValidation.Abstractions;

namespace CredentialsValidation.Shared
{
    /// <summary>
    /// The class representing the credential entity that encapsulates various sub fields
    /// </summary>
    public class Credentials : ICredentials
    {
        public string EMail { get; set; }

        public string Password { get; set; }
    }
}
