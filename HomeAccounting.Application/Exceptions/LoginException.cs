namespace HomeAccounting.Application.Exceptions
{
    public class LoginException : Exception
    {
        private const string message = "UserName or Password is wrong";
        public LoginException()
            : base(message) { }

        public LoginException(Exception innerException)
            : base(message, innerException) { }

    }
}
