using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Application.UseCases.Client.Commands;
using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Admin.Commands
{
    public class GreateCategoryCommad : ICommand<int>
    {
        public string Name { get; set; }
    }

    public class GreateCategoryCommadHandler : ICommandHandler<GreateCategoryCommad, int>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<ClientRegisterCommandHandler> _logger;

        public GreateCategoryCommadHandler(IApplicationDbContext dbContext, ILogger<ClientRegisterCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> Handle(GreateCategoryCommad command, CancellationToken cancellationToken)
        {
            var category = new Category();
            try
            {
                category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Name == command.Name, cancellationToken);

                if (category != null)
                {
                    throw new CategoryDublicationExceptions();
                }

                category = new Category() { Name = command.Name };
            }
            catch (CategoryDublicationExceptions ex)
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
                await _dbContext.Categories.AddAsync(category, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return category.Id;
        }
    }
}
