using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.DTOs;
using HomeAccounting.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Queries.ExistingTransactions
{
    public class GetExistingTransactionByIdQuery : IQuery<ResponseExistingTransactionViewModel>
    {
        public int Id { get; set; }
    }

    public class GetExistingTransactionByIdQueryHandler : IQueryHandler<GetExistingTransactionByIdQuery, ResponseExistingTransactionViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetExistingTransactionByIdQueryHandler> _logger;

        public GetExistingTransactionByIdQueryHandler(IApplicationDbContext context, ILogger<GetExistingTransactionByIdQueryHandler> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<ResponseExistingTransactionViewModel> Handle(GetExistingTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseExistingTransactionViewModel();
            try
            {
                var transaction = await _dbContext.Transactions
                    .Include(t => t.Client)
                    .Include(t => t.Category)
                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (transaction == null)
                {
                    throw new ExistingTransactionNotFoundExceptions();
                }

                response.Amount = transaction.Amount;
                response.Date = transaction.Date;
                response.TransactionType = transaction.TransactionType;
                response.Description = transaction.Description;
                response.ClientId = transaction.ClientId;
                response.CategoryName = transaction.Category.Name;

            }
            catch (ExistingTransactionNotFoundExceptions ex)
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
