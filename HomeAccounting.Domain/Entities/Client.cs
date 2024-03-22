namespace HomeAccounting.Domain.Entities
{
    public class Client : User
    {
        public Client()
        {
            Transactions = new HashSet<Transaction>();
        }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
