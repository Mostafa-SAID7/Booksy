namespace Booksy.Security
{
    /// <summary>
    /// Exception thrown when user is not authorized to perform action
    /// </summary>
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message)
        {
        }

        public AuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
