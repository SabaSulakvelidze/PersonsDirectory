using MediatR;

namespace PersonsDirectory.Application.Persons.Commands.RemoveRelatedPerson;

public sealed record RemoveRelatedPersonCommand(int PersonId, int RelatedPersonId) : IRequest;