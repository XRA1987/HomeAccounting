using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Application.UseCases.Client.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Admin.Commands
{
    public class UpdateCategoryCommand : ICommand<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<ClientRegisterCommandHandler> _logger;

        public UpdateCategoryCommandHandler(IApplicationDbContext dbContext, ILogger<ClientRegisterCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var catecory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                if (catecory == null)
                {
                    throw new CategoryNotFoundExceptions();
                }

                catecory.Name = command.Name ?? catecory.Name;
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
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
