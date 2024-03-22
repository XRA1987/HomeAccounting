using HomeAccounting.Application.UseCases.Auth.Commands;
using HomeAccounting.Application.UseCases.Client.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeAccounting.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsAdmin(LoginCommand command)
        {
            var token = await _mediator.Send(command);

            return Ok(token);
        }

        [HttpPost("Client/Register")]
        public async Task<IActionResult> RegisterAsClient(ClientRegisterCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}
