using HomeAccounting.Application.UseCases.Client.Commands.PlanningTransactions;
using HomeAccounting.Application.UseCases.Client.Queries.PlanningTransactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlanningTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPeriodReportPlanningTransactionWithCategories(int startMonth, int endMonth, int startYear, int endYear)
        {
            var response = await _mediator.Send(new GetPeriodReportPlanningTransactionQuery()
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
        public async Task<IActionResult> GetPlanningTransactionById(int id)
        {
            var response = await _mediator.Send(new GetPlanningTransactionByIdQuery() { Id = id });

            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePlanningTransaction(CreatePlanningTransactionCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePlanningTransaction(UpdatePlanningTransactionCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePlanningTransaction(int id)
        {
            var response = await _mediator.Send(new DeletePlanningTransactionCommand() { Id = id });

            return Ok(response);
        }
    }
}
