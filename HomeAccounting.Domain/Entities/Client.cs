namespace HomeAccounting.Domain.Entities
{
    public class Client : User
    {
        public Client()
        {
            Transactions = new HashSet<Transaction>();
        }
        public string FullName { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
