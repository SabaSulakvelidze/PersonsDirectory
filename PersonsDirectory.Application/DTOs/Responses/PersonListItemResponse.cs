using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Dtos;

public sealed class PersonListItemResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Gender Gender { get; set; }
    public string PersonalNumber { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string CityName { get; set; } = null!;
    public string? ImagePath { get; set; }
}