using MediatR;
using PersonsDirectory.Application.Reports.Dtos;

namespace PersonsDirectory.Application.Reports.Queries.RelatedPersonsReport;

public sealed record RelatedPersonsReportQuery
    : IRequest<IReadOnlyList<RelatedPersonsReportRow>>;