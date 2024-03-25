using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Client.Commands.PlanningTransactions
{
    public class DeletePlanningTransactionCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }

    public class DeletePlanningTransactionCommandHandler : ICommandHandler<DeletePlanningTransactionCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<DeletePlanningTransactionCommandHandler> _logger;

        public DeletePlanningTransactionCommandHandler(IApplicationDbContext dbContext, ILogger<DeletePlanningTransactionCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeletePlanningTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = new PlanningTransaction();
            try
            {
                transaction = await _dbContext.PlanningTransactions.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (transaction == null)
                {
                    throw new PlanningTransactionNotFoundExceptions();
                }
            }
            catch (PlanningTransactionNotFoundExceptions ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine($"Delete PlanningTransaction error: {ex.Message}");
            }
            finally
            {
                _dbContext.PlanningTransactions.Remove(transaction);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
