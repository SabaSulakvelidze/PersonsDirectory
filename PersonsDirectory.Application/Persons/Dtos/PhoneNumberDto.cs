using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Dtos;

public sealed class PhoneNumberDto
{
    public PhoneType Type { get; set; }
    public string Number { get; set; } = string.Empty;
}