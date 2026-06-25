using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonsDirectory.Application.Persons.Commands.AddRelatedPerson;
using PersonsDirectory.Application.Persons.Commands.RemoveRelatedPerson;

namespace PersonsDirectory.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class RelationsController(IMediator mediator) : ControllerBase
    {

        [HttpPost("{id}/AddRelated")]
        public async Task<IActionResult> AddRelated(int id, [FromBody] AddRelatedPersonCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { PersonId = id }, ct);
            return NoContent();
        }

        [HttpDelete("{id}/RemoveRelated/{relatedPersonId}")]
        public async Task<IActionResult> RemoveRelated(int id, int relatedPersonId, CancellationToken ct)
        {
            await mediator.Send(new RemoveRelatedPersonCommand(id, relatedPersonId), ct);
            return NoContent();
        }
    }
}
