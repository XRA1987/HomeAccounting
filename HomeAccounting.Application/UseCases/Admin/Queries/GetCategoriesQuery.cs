using HomeAccounting.Application.Abstractions;
using HomeAccounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAccounting.Application.UseCases.Admin.Queries
{
    public class GetCategoriesQuery : IQuery<List<Category>>
    {
        public int Page { get; set; }
        public int Limit { get; set; }

        public GetCategoriesQuery(int page, int limit)
        {
            Page = page;
            Limit = limit;
        }
    }

    public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, List<Category>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetCategoriesQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            if (request.Page <= 0 || request.Limit <= 0)
            {
                throw new ArgumentException("Page and Limit must be positive integers.");
            }

            var sKip = request.Page > 0 ? (request.Page - 1) * request.Limit : 0;

            var categories = await _dbContext.Categories
                .OrderBy(x => x.Id)
                .Skip(sKip)
                .Take(request.Limit)
                .ToListAsync(cancellationToken);

            return categories;
        }
    }


}
