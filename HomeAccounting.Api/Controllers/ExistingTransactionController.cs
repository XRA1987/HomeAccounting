using HomeAccounting.Application.UseCases.Client.Commands;
using HomeAccounting.Application.UseCases.Client.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistingTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExistingTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPeriodReportExistingTransactionWithCategories(int startMonth, int endMonth, int startYear, int endYear)
        {
            var response = await _mediator.Send(new GetPeriodReportExistingTransactionQuery()
            {
                StartMonth = startMonth,
                EndMonth = endMonth,
                StartYear = startYear,
                EndYear = endYear
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetExistingTransactionById(int id)
        {
            var response = await _mediator.Send(new GetExistingTransactionByIdQuery() { Id = id });

            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateExistingTransaction(CreateExistingTransactionCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateExistingTransaction(UpdateExistingTransactionCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExistingTransaction(int id)
        {
            var response = await _mediator.Send(new DeleteExistingTransactionCommand() { Id = id });

            return Ok(response);
        }
    }
}
