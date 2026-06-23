using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Dtos;

public sealed class PersonDetailsResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int CityId { get; set; }
    public string CityName { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public List<PhoneNumberDto> PhoneNumbers { get; set; } = new();
    public List<RelatedPersonResponse> RelatedPersons { get; set; } = new();
}