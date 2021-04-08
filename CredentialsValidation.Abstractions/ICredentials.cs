namespace CredentialsValidation.Abstractions
{
    public interface ICredentials
    {
        public string EMail { get; set; }

        public string Password { get; set; }
    }
}
