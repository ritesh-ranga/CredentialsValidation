namespace CredentialsValidation.Abstractions
{
    public interface IValidator
    {
        IResponseEnvelope Validate(ICredentials CredentialsToBeValidated);

        bool ValidateEmail(string Email, out Error error);

        bool ValidatePassword(string Password, out Error error);
    }
}
