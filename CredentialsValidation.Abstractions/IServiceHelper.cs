using System.Threading.Tasks;

namespace CredentialsValidation.Abstractions
{
    public interface IServiceHelper
    {
        Task<bool> ValidateCredentialsAsync(Credentials CredentialsToBeValidated);
    }
}
