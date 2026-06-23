using MediatR;
using PersonsDirectory.Application.Persons.Dtos;
using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Commands.UpdatePerson;

public sealed record UpdatePersonCommand(
    string FirstName,
    string LastName,
    Gender Gender,
    string PersonalNumber,
    DateOnly DateOfBirth,
    int CityId,
    List<PhoneNumberDto> PhoneNumbers) : IRequest
{
    public int Id { get; init; }
}