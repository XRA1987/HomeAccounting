namespace HomeAccounting.Application.DTOs
{
    public class PeriodReportPlanningTransactionViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string CategoryName { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpence { get; set; }
    }
}
