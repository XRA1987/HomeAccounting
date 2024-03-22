namespace HomeAccounting.Application.Exceptions
{
    public class CategoryDublicationExceptions : Exception
    {
        private const string alreadyExists = "Category already exist";

        public CategoryDublicationExceptions()
            : base(alreadyExists) { }

        public CategoryDublicationExceptions(Exception innerException)
            : base(alreadyExists, innerException) { }
    }
}
