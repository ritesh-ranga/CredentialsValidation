using CredentialsValidation.Abstractions;
using CredentialsValidation.Database.Abstractions;
using System;
using System.Linq;

namespace CredentialsValidation.Database
{
    public class SqlRepository : IRepository
    {
        private AppDbContext dbContext;

        public SqlRepository(AppDbContext appDbContext)
        {
            dbContext = appDbContext;
        }

        /// <summary>
        /// Method to save a new <see cref="ICredentials"/> to the database
        /// </summary>
        /// <param name="CredentialToBeAdded">The <see cref="ICredentials"/> to be saved</param>
        /// <returns><see cref="true"/> if the item was saved successfully, otherwise <see cref="false"/></returns>
        public bool AddCredentials(ICredentials CredentialToBeAdded)
        {
            bool successfullyAdded = false;

            try
            {
                dbContext.Credentials.Add(CredentialToBeAdded);
                dbContext.SaveChanges();
                successfullyAdded = true;
            }
            catch (Exception)
            {
                // Log the exception
            }

            return successfullyAdded;
        }

        /// <summary>
        /// Method to check whether a <see cref="ICredentials"/> already exists in the database
        /// </summary>
        /// <param name="CredentialToFind">The <see cref="ICredentials"/> to look up</param>
        /// <returns><see cref="true"/> if the passed <see cref="ICredentials"/> was found otherwise <see cref="false"/></returns>
        public bool CredentialsExists(ICredentials CredentialToFind)
        {
            bool credentialsExists = false;

            try
            {
                if (dbContext.Credentials.Any(x => x.EMail == CredentialToFind.EMail))
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
