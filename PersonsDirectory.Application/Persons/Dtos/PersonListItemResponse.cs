using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Dtos;
public sealed class PersonListItemResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string PersonalNumber { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string CityName { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
}