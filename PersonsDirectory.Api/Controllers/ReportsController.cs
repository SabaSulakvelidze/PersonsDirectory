using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonsDirectory.Application.Reports.Queries.RelatedPersonsReport;

namespace PersonsDirectory.WebApi.Controllers;

[ApiController]
[Route("api/reports")]
public sealed class ReportsController(IMediator mediator) : ControllerBase
{

    // Count of related persons per person, grouped by relation type.
    [HttpGet("related-persons")]
    public async Task<IActionResult> RelatedPersons(CancellationToken ct)
    { 
        return Ok(await mediator.Send(new RelatedPersonsReportQuery(), ct));
    }
}