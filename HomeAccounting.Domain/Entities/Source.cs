namespace HomeAccounting.Domain.Entities
{
    public class Source
    {
        public Source()
        {
            Transactions = new HashSet<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
