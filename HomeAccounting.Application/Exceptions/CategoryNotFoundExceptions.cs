using HomeAccounting.Domain.Exceptions;

namespace HomeAccounting.Application.Exceptions
{
    public class CategoryNotFoundExceptions : EntityNotFoundException
    {
        private const string _message = "Category";

        public CategoryNotFoundExceptions()
            : base(_message) { }
    }
}
