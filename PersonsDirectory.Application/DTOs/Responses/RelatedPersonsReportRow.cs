using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Reports.Dtos;

public sealed class RelatedPersonsReportRow
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public RelationType RelationType { get; set; }
    public int Count { get; set; }
}