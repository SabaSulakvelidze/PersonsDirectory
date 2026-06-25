using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Application.Persons.Commands.AddRelatedPerson;

public sealed class AddRelatedPersonHandler(IUnitOfWork _uow) : IRequestHandler<AddRelatedPersonCommand>
{
    public async Task Handle(AddRelatedPersonCommand request, CancellationToken ct)
    {
        if (request.PersonId == request.RelatedPersonId)
            throw new BadRequestException("A person cannot be related to themselves.");

        var person = await _uow.Persons.GetFullByIdAsync(request.PersonId, ct)
                     ?? throw new NotFoundException("Person", request.PersonId);

        var relatedPerson = await _uow.Persons.GetFullByIdAsync(request.RelatedPersonId, ct)
                     ?? throw new NotFoundException("Related person", request.RelatedPersonId);

        if (person.RelatedPersons.Any(r => r.RelatedPersonId == request.RelatedPersonId))
            throw new ConflictException("These persons are already related.");

        person.RelatedPersons.Add(new PersonRelation
        {
            PersonId = request.PersonId,
            RelatedPersonId = request.RelatedPersonId,
            RelationType = request.RelationType
        });

        relatedPerson.RelatedPersons.Add(new PersonRelation
        {
            PersonId = request.RelatedPersonId,
            RelatedPersonId = request.PersonId,
            RelationType = request.RelationType
        });

        await _uow.SaveChangesAsync(ct);
    }
}