using CredentialsValidation.Abstractions;

namespace CredentialsValidation.Database.Abstractions
{
    public interface IRepository
    {
        bool AddCredentials(ICredentials CredentialToBeAdded);

        bool CredentialsExists(ICredentials CredentialToFind);
    }
}
