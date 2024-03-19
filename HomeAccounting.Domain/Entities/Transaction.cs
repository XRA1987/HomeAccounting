using HomeAccounting.Domain.Enums;

namespace HomeAccounting.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public int SourceId { get; set; }

        public Client Client { get; set; }
        public Source Source { get; set; }
    }
}
