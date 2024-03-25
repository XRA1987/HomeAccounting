using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Entities;
using HomeAccounting.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Commands.ExistingTransactions
{
    public class UpdateExistingTransactionCommand : ICommand<Unit>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateExistingTransactionCommandHandler : ICommandHandler<UpdateExistingTransactionCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<CreateExistingTransactionCommandHandler> _logger;

        public UpdateExistingTransactionCommandHandler(IApplicationDbContext dbContext,
            ILogger<CreateExistingTransactionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateExistingTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = new ExistingTransaction();
            try
            {
                transaction = await _dbContext.ExistingTransactions.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (transaction == null)
                {
                    throw new ExistingTransactionNotFoundExceptions();
                }

                transaction.Amount = command.Amount == 0 ? transaction.Amount : command.Amount;
                transaction.Date = command.Date;
                transaction.TransactionType = command.TransactionType == null ? transaction.TransactionType : command.TransactionType;
                transaction.Description = command.Description ?? transaction.Description;
                transaction.CategoryId = command.CategoryId == 0 ? transaction.CategoryId : command.CategoryId;
            }
            catch (ExistingTransactionNotFoundExceptions ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Update ExistingTransaction error: {ex.Message}");
            }
            finally
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
