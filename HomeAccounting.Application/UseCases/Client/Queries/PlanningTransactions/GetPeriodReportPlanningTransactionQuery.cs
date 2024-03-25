using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.DTOs;
using HomeAccounting.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Queries.PlanningTransactions
{
    public class GetPeriodReportPlanningTransactionQuery : IQuery<List<PeriodReportPlanningTransactionViewModel>>
    {
        public int StartYear { get; set; }
        public int StartMonth { get; set; }
        public int EndYear { get; set; }
        public int EndMonth { get; set; }
    }

    public class GetPeriodReportPlanningTransactionQueryHandler : IQueryHandler<GetPeriodReportPlanningTransactionQuery, List<PeriodReportPlanningTransactionViewModel>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetPeriodReportPlanningTransactionQueryHandler> _logger;

        public GetPeriodReportPlanningTransactionQueryHandler(IApplicationDbContext dbContext,
            ICurrentUserService currentUserService,
            ILogger<GetPeriodReportPlanningTransactionQueryHandler> logger)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<List<PeriodReportPlanningTransactionViewModel>> Handle(GetPeriodReportPlanningTransactionQuery request, CancellationToken cancellationToken)
        {
            var reports = new List<PeriodReportPlanningTransactionViewModel>();

            try
            {
                if (request.StartMonth < 1 || request.StartMonth > 12 || request.EndMonth < 1 || request.EndMonth > 12)
                {
                    throw new ArgumentOutOfRangeException("startMonth or endMonth", "Month must be between 1 and 12.");
                }

                if (request.StartYear > request.EndYear || request.StartYear == request.EndYear && request.StartMonth > request.EndMonth)
                {
                    throw new ArgumentException("Start date cannot be after end date.");
                }

                for (int year = request.StartYear; year <= request.EndYear; year++)
                {
                    for (int month = year == request.StartYear ? request.StartMonth : 1; month <= (year == request.EndYear ? request.EndMonth : 12); month++)
                    {
                        var clientTransactions = await _dbContext.PlanningTransactions
                            .Where(c => c.ClientId == _currentUserService.UserId)
                            .Where(t => t.Date.Year == year && t.Date.Month == month)
                            .ToListAsync(cancellationToken);

                        if (!clientTransactions.Any())
                        {
                            continue;
                        }

                        var transactionsByCategory = clientTransactions.GroupBy(t => t.CategoryId);

                        foreach (var group in transactionsByCategory)
                        {
                            var categoryName = _dbContext.Categories.FirstOrDefault(c => c.Id == group.Key)?.Name ?? "Uncategorized";

                            var categoryIncome = group.Where(t => t.TransactionType == TransactionType.Income)
                                .Sum(t => t.Amount);
                            var categoryExpense = group.Where(t => t.TransactionType == TransactionType.Expense)
                                .Sum(t => t.Amount);

                            reports.Add(new PeriodReportPlanningTransactionViewModel
                            {
                                Month = month,
                                Year = year,
                                CategoryName = categoryName,
                                TotalIncome = categoryIncome,
                                TotalExpence = categoryExpense
                            });
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Invalid month provided: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Start date cannot be after end date: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Error retrieving transactions: {ex.Message}");
            }

            return reports;
        }
    }
}
