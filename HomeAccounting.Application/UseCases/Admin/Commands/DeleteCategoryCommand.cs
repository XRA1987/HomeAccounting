using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.UseCases.Admin.Commands
{
    public class DeleteCategoryCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundExceptions();
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
