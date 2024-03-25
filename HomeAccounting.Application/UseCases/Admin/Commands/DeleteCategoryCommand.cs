using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Application.UseCases.Client.Commands;
using HomeAccounting.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Admin.Commands
{
    public class DeleteCategoryCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<ClientRegisterCommandHandler> _logger;

        public DeleteCategoryCommandHandler(IApplicationDbContext dbContext, ILogger<ClientRegisterCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = new Category();
            try
            {
                category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (category == null)
                {
                    throw new CategoryNotFoundExceptions();
                }
            }
            catch (CategoryNotFoundExceptions ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
