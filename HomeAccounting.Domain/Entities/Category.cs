namespace HomeAccounting.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            Transactions = new HashSet<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
