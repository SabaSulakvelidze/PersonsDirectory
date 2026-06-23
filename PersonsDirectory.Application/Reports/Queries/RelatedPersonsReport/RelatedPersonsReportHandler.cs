using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Reports.Dtos;

namespace PersonsDirectory.Application.Reports.Queries.RelatedPersonsReport;

public sealed class RelatedPersonsReportHandler(IUnitOfWork _uow)
    : IRequestHandler<RelatedPersonsReportQuery, IReadOnlyList<RelatedPersonsReportRow>>
{
    public async Task<IReadOnlyList<RelatedPersonsReportRow>> Handle(
        RelatedPersonsReportQuery request, CancellationToken ct)
    {
        // Group relations by owner + type, count each, project to rows.
        var rows = await _uow.Persons.Query()
            .SelectMany(p => p.RelatedPersons)
            .GroupBy(rel => new
            {
                rel.PersonId,
                rel.Person.FirstName,
                rel.Person.LastName,
                rel.RelationType
            })
            .Select(g => new RelatedPersonsReportRow
            {
                PersonId = g.Key.PersonId,
                FirstName = g.Key.FirstName,
                LastName = g.Key.LastName,
                RelationType = g.Key.RelationType,
                Count = g.Count()
            })
            .OrderBy(x => x.PersonId).ThenBy(x => x.RelationType)
            .ToListAsync(ct);

        return rows;
    }
}