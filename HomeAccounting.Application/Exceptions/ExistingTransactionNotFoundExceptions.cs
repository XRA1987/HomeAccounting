using HomeAccounting.Domain.Exceptions;

namespace HomeAccounting.Application.Exceptions
{
    public class ExistingTransactionNotFoundExceptions : EntityNotFoundExceptions
    {
        private const string _message = "ExistingTransaction";

        public ExistingTransactionNotFoundExceptions()
            : base(_message) { }
    }
}
