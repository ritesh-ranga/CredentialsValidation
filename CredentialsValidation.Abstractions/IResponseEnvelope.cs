namespace CredentialsValidation.Abstractions
{
    public interface IResponseEnvelope
    {
        /// <summary>
        /// Flag representing if the call was a success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A message, if the request was a success
        /// </summary>
        public string SuccessMessage { get; set; }

        /// <summary>
        /// An <see cref="ErrorCollection"/> containing a list of errors, if the request was a failure
        /// </summary>
        public ErrorCollection Errors { get; set; }
    }
}
