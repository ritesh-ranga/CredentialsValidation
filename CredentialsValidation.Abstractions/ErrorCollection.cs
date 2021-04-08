using System.Collections.ObjectModel;

namespace CredentialsValidation.Abstractions
{
    /// <summary>
    /// The list of errors that occured while validating the <see cref="ICredentials"/>
    /// </summary>
    public class ErrorCollection : Collection<Error>
    {
    }
}
