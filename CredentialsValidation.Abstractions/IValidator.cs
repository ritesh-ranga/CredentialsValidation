namespace CredentialsValidation.Abstractions
{
    public interface IValidator
    {
        bool ValidateEmail(string Email);

        bool ValidatePassword(string Password);
    }
}
