using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Persons.Mapping;

namespace PersonsDirectory.Application.Persons.Commands.CreatePerson;

public sealed class CreatePersonHandler(IUnitOfWork _uow) : IRequestHandler<CreatePersonCommand, int>
{
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken ct)
    {
        // Referential checks the validator can't do (need the DB).
        if (!await _uow.Cities.ExistsAsync(c => c.Id == request.CityId, ct))
            throw new BadRequestException("Selected city does not exist.");

        if (await _uow.Persons.ExistsAsync(p => p.PersonalNumber == request.PersonalNumber, ct))
            throw new ConflictException("A person with this personal number already exists.");

        var entity = PersonMapper.ToEntity(request);
        await _uow.Persons.AddAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);

        return entity.Id;
    }
}