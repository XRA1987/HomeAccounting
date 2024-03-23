using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.DTOs;
using HomeAccounting.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.UseCases.Client.Queries
{
    public class GetExistingTransactionByIdQuery : IQuery<ResponseExistingTransactionViewModel>
    {
        public int Id { get; set; }
    }

    public class GetExistingTransactionByIdQueryHandler : IQueryHandler<GetExistingTransactionByIdQuery, ResponseExistingTransactionViewModel>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetExistingTransactionByIdQueryHandler(IApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<ResponseExistingTransactionViewModel> Handle(GetExistingTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseExistingTransactionViewModel();
            try
            {
                var transaction = await _dbContext.Transactions
                    .Include(t => t.Client)
                    .Include(t => t.Category)
                    .FirstOrDefaultAsync(t => t.Id == request.Id);

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
                Console.WriteLine($"Error retrieving transaction: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving transaction: {ex.Message}");
            }

            return response;
        }
    }
}
