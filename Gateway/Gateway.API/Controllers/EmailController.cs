using System.Threading.Tasks;
using Gateway.Domain.Features.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmail.Command command)
        {
            await _mediator.Send(command, default);
            return Ok();
        }
    }
}
