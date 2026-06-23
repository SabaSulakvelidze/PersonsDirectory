using MediatR;
using PersonsDirectory.Application.Persons.Dtos;
using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Commands.CreatePerson;

public sealed record CreatePersonCommand(
    string FirstName,
    string LastName,
    Gender Gender,
    string PersonalNumber,
    DateOnly DateOfBirth,
    int CityId,
    List<PhoneNumberDto> PhoneNumbers) : IRequest<int>;