using HomeAccounting.Domain.Enums;

namespace HomeAccounting.Application.DTOs
{
    public class ResponsePlanningTransactionViewModel
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public string CategoryName { get; set; }
        public string IsDane { get; set; }
    }
}
