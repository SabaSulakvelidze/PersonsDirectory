using MediatR;
using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Commands.AddRelatedPerson;

public sealed record AddRelatedPersonCommand(
    int RelatedPersonId,
    RelationType RelationType) : IRequest
{
    public int PersonId { get; init; }
}