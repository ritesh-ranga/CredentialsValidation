using CredentialsValidation.Abstractions;

namespace CredentialsValidation.Shared
{
    public class ResponseEnvelope : IResponseEnvelope
    {
        public bool Success { get; set; }
        public string SuccessMessage { get; set; }
        public ErrorCollection Errors { get; set; }
    }
}
