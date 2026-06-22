using MediatR;

namespace PersonsDirectory.Application.Common.Commands.RemoveRelatedPerson;

public sealed record RemoveRelatedPersonCommand(int PersonId, int RelatedPersonId) : IRequest;