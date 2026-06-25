using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonsDirectory.Application.Persons.Commands.CreatePerson;
using PersonsDirectory.Application.Persons.Commands.DeletePerson;
using PersonsDirectory.Application.Persons.Commands.UpdatePerson;
using PersonsDirectory.Application.Persons.Commands.UploadImage;
using PersonsDirectory.Application.Persons.Dtos;
using PersonsDirectory.Application.Persons.Queries.DetailedSearch;
using PersonsDirectory.Application.Persons.Queries.GetPersonById;
using PersonsDirectory.Application.Persons.Queries.QuickSearch;

namespace PersonsDirectory.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class PersonsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command, CancellationToken ct)
        {
            var id = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonCommand command, CancellationToken ct)
        {
            await mediator.Send(command with { Id = id }, ct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await mediator.Send(new DeletePersonCommand(id), ct);
            return NoContent();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDetailsResponse>> GetById(int id, CancellationToken ct)
        {
            return Ok(await mediator.Send(new GetPersonByIdQuery(id), ct));
        }

        [HttpGet("quick-search")]
        public async Task<IActionResult> QuickSearch([FromQuery] QuickSearchQuery query, CancellationToken ct)
        {
            return Ok(await mediator.Send(query, ct));
        }

        [HttpGet("search")]
        public async Task<IActionResult> DetailedSearch([FromQuery] DetailedSearchQuery query, CancellationToken ct)
        {
            return Ok(await mediator.Send(query, ct));
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file, CancellationToken ct)
        {
            if (file is null || file.Length == 0)
                return BadRequest(new { message = "File is required." });

            await using var stream = file.OpenReadStream();
            var path = await mediator.Send(new UploadImageCommand(id, stream, file.FileName), ct);
            return Ok(new { imagePath = path });
        }

        

    }
}
