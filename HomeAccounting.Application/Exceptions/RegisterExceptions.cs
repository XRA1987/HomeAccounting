namespace HomeAccounting.Application.Exceptions
{
    public class RegisterExceptions : Exception
    {
        private const string _message = "Registration error";

        public RegisterExceptions()
            : base(_message) { }

        public RegisterExceptions(Exception innerException)
            : base(_message, innerException) { }
    }
}
