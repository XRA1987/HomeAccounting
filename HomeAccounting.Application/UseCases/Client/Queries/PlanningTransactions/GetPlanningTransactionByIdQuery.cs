using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.DTOs;
using HomeAccounting.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Queries.PlanningTransactions
{
    public class GetPlanningTransactionByIdQuery : IQuery<ResponsePlanningTransactionViewModel>
    {
        public int Id { get; set; }
    }

    public class GetPlanningTransactionByIdQueryHandler : IQueryHandler<GetPlanningTransactionByIdQuery, ResponsePlanningTransactionViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetPlanningTransactionByIdQueryHandler> _logger;

        public GetPlanningTransactionByIdQueryHandler(IApplicationDbContext context, ILogger<GetPlanningTransactionByIdQueryHandler> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<ResponsePlanningTransactionViewModel> Handle(GetPlanningTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponsePlanningTransactionViewModel();
            try
            {
                var transaction = await _dbContext.Transactions
                    .Include(t => t.Client)
                    .Include(t => t.Category)
                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (transaction == null)
                {
                    throw new PlanningTransactionNotFoundExceptions();
                }

                response.Amount = transaction.Amount;
                response.Date = transaction.Date;
                response.TransactionType = transaction.TransactionType;
                response.Description = transaction.Description;
                response.ClientId = transaction.ClientId;
                response.CategoryName = transaction.Category.Name;

            }
            catch (PlanningTransactionNotFoundExceptions ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Error retrieving transaction: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Error retrieving transaction: {ex.Message}");
            }

            return response;
        }
    }
}
