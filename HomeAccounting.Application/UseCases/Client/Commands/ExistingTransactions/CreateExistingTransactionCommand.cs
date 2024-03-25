using HomeAccounting.Application.Abstractions;
using HomeAccounting.Domain.Entities;
using HomeAccounting.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Commands.ExistingTransactions
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
        private readonly ILogger<CreateExistingTransactionCommandHandler> _logger;

        public CreateExistingTransactionCommandHandler(IApplicationDbContext dbContext,
            ICurrentUserService currentUserService,
            ILogger<CreateExistingTransactionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateExistingTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = new ExistingTransaction();
            try
            {
                transaction.Amount = command.Amount;
                transaction.Date = command.Date;
                transaction.TransactionType = command.TransactionType;
                transaction.Description = command.Description;
                transaction.ClientId = _currentUserService.UserId;
                transaction.CategoryId = command.CategoryId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Create ExistingTransaction error: {ex.Message}");
            }
            finally
            {
                await _dbContext.ExistingTransactions.AddAsync(transaction, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
