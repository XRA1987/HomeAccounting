using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.DTOs;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Application.UseCases.Client.Commands;
using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Admin.Queries
{
    public class GetCategoryByIdQuery : IQuery<ResponseCategoryViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, ResponseCategoryViewModel>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<ClientRegisterCommandHandler> _logger;

        public GetCategoryByIdQueryHandler(IApplicationDbContext context, ILogger<ClientRegisterCommandHandler> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<ResponseCategoryViewModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = new Category();
            try
            {
                category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

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
                Console.WriteLine($"Error finding categoriess: {ex.Message}");
            }

            return new ResponseCategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
    }
}

