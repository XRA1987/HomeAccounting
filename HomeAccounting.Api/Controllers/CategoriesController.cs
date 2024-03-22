﻿using HomeAccounting.Application.UseCases.Admin.Commands;
using HomeAccounting.Application.UseCases.Admin.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(int page, int limit)
        {
            var response = await _mediator.Send(new GetCategoriesQuery(page, limit));

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "AdminActions")]
        public async Task<IActionResult> Create(GreateCategoryCommad command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Policy = "AdminActions")]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminActions")]
        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }


    }
}
