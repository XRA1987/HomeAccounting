using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.DTOs;
using HomeAccounting.Application.Exceptions;
using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.UseCases.Admin.Queries
{
    public class GetCategoryByIdQuery : IQuery<ResponseCategoryViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, ResponseCategoryViewModel>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetCategoryByIdQueryHandler(IApplicationDbContext context)
        {
            _dbContext = context;
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
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
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

