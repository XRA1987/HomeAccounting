using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.UseCases.Client.Commands
{
    public class DeleteExistingTransactionCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteExistingTransactionCommandHandler : ICommandHandler<DeleteExistingTransactionCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteExistingTransactionCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteExistingTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.ExistingTransactions.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (transaction == null)
            {
                throw new ExistingTransactionNotFoundExceptions();
            }

            _dbContext.ExistingTransactions.Remove(transaction);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
