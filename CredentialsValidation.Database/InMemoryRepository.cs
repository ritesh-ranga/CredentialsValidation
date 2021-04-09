using CredentialsValidation.Abstractions;
using CredentialsValidation.Database.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace CredentialsValidation.Database
{
    public class InMemoryRepository : IRepository
    {
        private static ConcurrentDictionary<string, ICredentials> _Credentials =
             new ConcurrentDictionary<string, ICredentials>();

        /// <summary>
        /// Method to add a new <see cref="ICredentials"/> to the collection
        /// </summary>
        /// <param name="Credentials">The <see cref="ICredentials"/> to be added</param>
        /// <returns><see cref="true"/> if the item was added successfully, otherwise <see cref="false"/></returns>
        public bool AddCredentials(ICredentials CredentialToBeAdded)
        {
            bool successfullyAdded = false;

            try
            {
                string Key = Guid.NewGuid().ToString();
                successfullyAdded = _Credentials.TryAdd(Key, CredentialToBeAdded);
            }
            catch (Exception)
            {
                // Log the exception
            }

            return successfullyAdded;
        }

        /// <summary>
        /// Method to check whether a <see cref="ICredentials"/> already exists in the collection
        /// </summary>
        /// <param name="CredentialToFind">The <see cref="ICredentials"/> to look up</param>
        /// <returns><see cref="true"/> if the passed <see cref="ICredentials"/> was found otherwise <see cref="false"/></returns>
        public bool CredentialsExists(ICredentials CredentialToFind)
        {
            bool credentialsExists = false;

            try
            {
                if (_Credentials.Values.Any(x => x.EMail == CredentialToFind.EMail))
                    credentialsExists = true;
            }
            catch (Exception)
            {
                // Log the exception
            }

            return credentialsExists;
        }
    }
}
