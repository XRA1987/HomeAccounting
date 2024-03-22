using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        public async Task<Unit> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var catecory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (catecory == null)
            {
                throw new CategoryNotFoundExceptions();
            }

            catecory.Name = command.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
