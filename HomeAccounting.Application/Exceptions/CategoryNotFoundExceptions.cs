using HomeAccounting.Domain.Exceptions;

namespace HomeAccounting.Application.Exceptions
{
    public class CategoryNotFoundExceptions : EntityNotFoundExceptions
    {
        private const string _message = "Category";

        public CategoryNotFoundExceptions()
            : base(_message) { }
    }
}
