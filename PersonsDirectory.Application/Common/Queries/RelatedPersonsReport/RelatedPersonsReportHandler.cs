using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Reports.Dtos;

namespace PersonsDirectory.Application.Common.Queries.RelatedPersonsReport;

public sealed class RelatedPersonsReportHandler(IUnitOfWork uow)
    : IRequestHandler<RelatedPersonsReportQuery, IReadOnlyList<RelatedPersonsReportRow>>
{
    public async Task<IReadOnlyList<RelatedPersonsReportRow>> Handle(
        RelatedPersonsReportQuery request, CancellationToken ct)
    {
        var rows = await uow.Persons.Query()
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