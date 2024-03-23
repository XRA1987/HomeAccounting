namespace HomeAccounting.Domain.Exceptions
{
    public class EntityNotFoundExceptions : Exception
    {
        public EntityNotFoundExceptions(string entityName)
            : base($"{entityName} not found")
        {
        }
    }
}
