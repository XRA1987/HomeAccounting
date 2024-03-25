using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Entities;
using HomeAccounting.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Commands.PlanningTransactions
{
    public class UpdatePlanningTransactionCommand : ICommand<Unit>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsDone { get; set; }
    }

    public class UpdatePlanningTransactionCommandHandler : ICommandHandler<UpdatePlanningTransactionCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<CreatePlanningTransactionCommandHandler> _logger;

        public UpdatePlanningTransactionCommandHandler(IApplicationDbContext dbContext,
            ILogger<CreatePlanningTransactionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdatePlanningTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = new PlanningTransaction();
            try
            {
                transaction = await _dbContext.PlanningTransactions.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (transaction == null)
                {
                    throw new PlanningTransactionNotFoundExceptions();
                }

                transaction.Amount = command.Amount == 0 ? transaction.Amount : command.Amount;
                transaction.Date = command.Date;
                transaction.TransactionType = command.TransactionType == null ? transaction.TransactionType : command.TransactionType;
                transaction.Description = command.Description ?? transaction.Description;
                transaction.CategoryId = command.CategoryId == 0 ? transaction.CategoryId : command.CategoryId;
                transaction.IsDone = command.IsDone;
            }
            catch (PlanningTransactionNotFoundExceptions ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Update PlanningTransaction error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Update PlanningTransaction error: {ex.Message}");
            }
            finally
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
