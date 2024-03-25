using HomeAccounting.Application.Abstractions;
using HomeAccounting.Domain.Entities;
using HomeAccounting.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Commands.PlanningTransactions
{
    public class CreatePlanningTransactionCommand : ICommand<Unit>
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreatePlanningTransactionCommandHandler : ICommandHandler<CreatePlanningTransactionCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<CreatePlanningTransactionCommandHandler> _logger;

        public CreatePlanningTransactionCommandHandler(IApplicationDbContext dbContext,
            ICurrentUserService currentUserService,
            ILogger<CreatePlanningTransactionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreatePlanningTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = new PlanningTransaction();
            try
            {
                transaction.Amount = command.Amount;
                transaction.Date = command.Date;
                transaction.TransactionType = command.TransactionType;
                transaction.Description = command.Description;
                transaction.ClientId = _currentUserService.UserId;
                transaction.CategoryId = command.CategoryId;
                transaction.IsDone = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Create ExistingTransaction error: {ex.Message}");
            }
            finally
            {
                await _dbContext.PlanningTransactions.AddAsync(transaction, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
