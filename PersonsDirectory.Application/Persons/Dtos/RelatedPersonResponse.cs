using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Dtos;

public sealed class RelatedPersonResponse
{
    public int RelatedPersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public RelationType RelationType { get; set; }
}