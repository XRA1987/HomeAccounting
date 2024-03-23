using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.UseCases.Client.Commands
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

        public UpdateExistingTransactionCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateExistingTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.ExistingTransactions.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (transaction == null)
            {
                throw new ExistingTransactionNotFoundExceptions();
            }

            transaction.Amount = command.Amount == 0 ? transaction.Amount : command.Amount;
            transaction.Date = command.Date == null ? transaction.Date : command.Date;
            transaction.TransactionType = command.TransactionType == null ? transaction.TransactionType : command.TransactionType;
            transaction.Description = command.Description ?? transaction.Description;
            transaction.CategoryId = command.CategoryId == 0 ? transaction.CategoryId : command.CategoryId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
