using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Mapping;

namespace PersonsDirectory.Application.Common.Commands.CreatePerson;

public sealed class CreatePersonHandler(IUnitOfWork uow) : IRequestHandler<CreatePersonCommand, int>
{
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken ct)
    {
        var data = request.Data;

        // Referential checks the validator can't do (need the DB).
        if (!await uow.Cities.ExistsAsync(c => c.Id == data.CityId, ct))
            throw new BadRequestException("Selected city does not exist.");

        if (await uow.Persons.ExistsAsync(p => p.PersonalNumber == data.PersonalNumber, ct))
            throw new ConflictException("A person with this personal number already exists.");

        var entity = PersonMapper.ToEntity(data);
        await uow.Persons.AddAsync(entity, ct);
        await uow.SaveChangesAsync(ct);

        return entity.Id;
    }
}