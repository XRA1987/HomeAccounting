using HomeAccounting.Application.Abstractions;
using HomeAccounting.Application.DTOs;
using HomeAccounting.Application.UseCases.Client.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Application.UseCases.Admin.Queries
{
    public class GetCategoriesQuery : IQuery<List<ResponseCategoryViewModel>>
    {
        public int Page { get; set; }
        public int Limit { get; set; }

        public GetCategoriesQuery(int page, int limit)
        {
            Page = page;
            Limit = limit;
        }
    }

    public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, List<ResponseCategoryViewModel>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<ClientRegisterCommandHandler> _logger;

        public GetCategoriesQueryHandler(IApplicationDbContext dbContext, ILogger<ClientRegisterCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<ResponseCategoryViewModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Page <= 0 || request.Limit <= 0)
                {
                    throw new ArgumentException("Page and Limit must be positive integers.");
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

            var sKip = request.Page > 0 ? (request.Page - 1) * request.Limit : 0;

            return await _dbContext.Categories
                .Select(x => new ResponseCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .OrderBy(x => x.Id)
                .Skip(sKip)
                .Take(request.Limit)
                .ToListAsync(cancellationToken);
        }
    }
}
