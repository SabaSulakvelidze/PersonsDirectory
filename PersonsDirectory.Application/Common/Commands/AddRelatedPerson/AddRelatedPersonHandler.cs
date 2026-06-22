using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Application.Common.Commands.AddRelatedPerson;

public sealed class AddRelatedPersonHandler(IUnitOfWork uow) : IRequestHandler<AddRelatedPersonCommand>
{
    public async Task Handle(AddRelatedPersonCommand request, CancellationToken ct)
    {
        var data = request.Data;

        if (request.PersonId == data.RelatedPersonId)
            throw new BadRequestException("A person cannot be related to themselves.");

        var person = await uow.Persons.GetFullByIdAsync(request.PersonId, ct)
                     ?? throw new NotFoundException("Person", request.PersonId);

        if (!await uow.Persons.ExistsAsync(p => p.Id == data.RelatedPersonId, ct))
            throw new NotFoundException("Related person", data.RelatedPersonId);

        if (person.RelatedPersons.Any(r => r.RelatedPersonId == data.RelatedPersonId))
            throw new ConflictException("These persons are already related.");

        person.RelatedPersons.Add(new PersonRelation
        {
            PersonId = request.PersonId,
            RelatedPersonId = data.RelatedPersonId,
            RelationType = data.RelationType
        });

        await uow.SaveChangesAsync(ct);
    }
}