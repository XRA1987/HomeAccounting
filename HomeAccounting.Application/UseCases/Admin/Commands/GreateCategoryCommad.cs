using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.UseCases.Admin.Commands
{
    public class GreateCategoryCommad : ICommand<int>
    {
        public string Name { get; set; }
    }

    public class GreateCategoryCommadHandler : ICommandHandler<GreateCategoryCommad, int>
    {
        private readonly IApplicationDbContext _dbContext;

        public GreateCategoryCommadHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(GreateCategoryCommad command, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Name == command.Name, cancellationToken);

            if (category != null)
            {
                throw new CategoryDublicationExceptions();
            }

            category = new Category() { Name = command.Name };

            await _dbContext.Categories.AddAsync(category, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
