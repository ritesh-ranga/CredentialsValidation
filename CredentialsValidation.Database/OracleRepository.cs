using CredentialsValidation.Abstractions;
using CredentialsValidation.Database.Abstractions;

namespace CredentialsValidation.Database
{
    public class OracleRepository : IRepository
    {
        public bool CredentialsExists(ICredentials CredentialsToFind)
        {
            // Implementation for credential lookup
            return true;
        }

        public bool AddCredentials(ICredentials Credentials)
        {
            // Implementation to add credentials
            return true;
        }
    }
}
