using HomeAccounting.Application.Abstractions;
using HomeAccounting.Domain.Entities;
using HomeAccounting.Domain.Enums;
using MediatR;

namespace HomeAccounting.Application.UseCases.Client.Commands
{
    public class CreateExistingTransactionCommand : ICommand<Unit>
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateExistingTransactionCommandHandler : ICommandHandler<CreateExistingTransactionCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public CreateExistingTransactionCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(CreateExistingTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = new ExistingTransaction()
            {
                Amount = command.Amount,
                Date = command.Date,
                TransactionType = command.TransactionType,
                Description = command.Description,
                ClientId = _currentUserService.UserId,
                CategoryId = command.CategoryId
            };

            await _dbContext.ExistingTransactions.AddAsync(transaction, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
