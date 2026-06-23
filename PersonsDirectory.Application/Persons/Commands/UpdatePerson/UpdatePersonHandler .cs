using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Persons.Mapping;

namespace PersonsDirectory.Application.Persons.Commands.UpdatePerson;

public sealed class UpdatePersonHandler(IUnitOfWork _uow) : IRequestHandler<UpdatePersonCommand>
{
    public async Task Handle(UpdatePersonCommand request, CancellationToken ct)
    {
        var person = await _uow.Persons.GetFullByIdAsync(request.Id, ct)
                     ?? throw new NotFoundException("Person", request.Id);

        if (!await _uow.Cities.ExistsAsync(c => c.Id == request.CityId, ct))
            throw new BadRequestException("Selected city does not exist.");

        // Unique personal number, excluding this same person.
        if (await _uow.Persons.ExistsAsync(p =>
                p.PersonalNumber == request.PersonalNumber && p.Id != request.Id, ct))
            throw new ConflictException("A person with this personal number already exists.");

        PersonMapper.ApplyUpdate(person, request);
        await _uow.SaveChangesAsync(ct);
    }
}