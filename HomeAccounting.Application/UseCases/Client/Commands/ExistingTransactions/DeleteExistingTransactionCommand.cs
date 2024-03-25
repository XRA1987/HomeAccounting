using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Commands.ExistingTransactions
{
    public class DeleteExistingTransactionCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteExistingTransactionCommandHandler : ICommandHandler<DeleteExistingTransactionCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<CreateExistingTransactionCommandHandler> _logger;

        public DeleteExistingTransactionCommandHandler(IApplicationDbContext dbContext, ILogger<CreateExistingTransactionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteExistingTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = new ExistingTransaction();
            try
            {
                transaction = await _dbContext.ExistingTransactions.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (transaction == null)
                {
                    throw new ExistingTransactionNotFoundExceptions();
                }
            }
            catch (ExistingTransactionNotFoundExceptions ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Delete ExistingTransaction error: {ex.Message}");
            }
            finally
            {
                _dbContext.ExistingTransactions.Remove(transaction);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
