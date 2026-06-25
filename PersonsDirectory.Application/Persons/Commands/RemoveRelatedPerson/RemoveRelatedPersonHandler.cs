using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;

namespace PersonsDirectory.Application.Persons.Commands.RemoveRelatedPerson;

public sealed class RemoveRelatedPersonHandler(IUnitOfWork _uow) : IRequestHandler<RemoveRelatedPersonCommand>
{
    public async Task Handle(RemoveRelatedPersonCommand request, CancellationToken ct)
    {
        var person = await _uow.Persons.GetFullByIdAsync(request.PersonId, ct)
                     ?? throw new NotFoundException("Person", request.PersonId);

        var relation = person.RelatedPersons
            .FirstOrDefault(r => r.RelatedPersonId == request.RelatedPersonId)
            ?? throw new NotFoundException("Relation", request.RelatedPersonId);

        person.RelatedPersons.Remove(relation);


        var relatedPerson = await _uow.Persons.GetFullByIdAsync(request.RelatedPersonId, ct);

        var relatedPersonRelation = relatedPerson?.RelatedPersons
            .FirstOrDefault(r => r.RelatedPersonId == request.PersonId);

        if(relatedPersonRelation is not null)
            relatedPerson!.RelatedPersons.Remove(relatedPersonRelation);
        await _uow.SaveChangesAsync(ct);

    }
}