using HomeAccounting.Domain.Exceptions;

namespace HomeAccounting.Application.Exceptions
{
    public class PlanningTransactionNotFoundExceptions : EntityNotFoundExceptions
    {
        private const string _message = "PlanningTransaction";

        public PlanningTransactionNotFoundExceptions()
            : base(_message) { }
    }
}
