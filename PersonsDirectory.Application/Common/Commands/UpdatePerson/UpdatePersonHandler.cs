using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Mapping;

namespace PersonsDirectory.Application.Common.Commands.UpdatePerson;

public sealed class UpdatePersonHandler(IUnitOfWork uow) : IRequestHandler<UpdatePersonCommand>
{

    public async Task Handle(UpdatePersonCommand request, CancellationToken ct)
    {
        var person = await uow.Persons.GetFullByIdAsync(request.Id, ct)
                     ?? throw new NotFoundException("Person", request.Id);

        if (!await uow.Cities.ExistsAsync(c => c.Id == request.Data.CityId, ct))
            throw new BadRequestException("Selected city does not exist.");

        // Unique personal number, excluding this same person.
        if (await uow.Persons.ExistsAsync(p =>
                p.PersonalNumber == request.Data.PersonalNumber && p.Id != request.Id, ct))
            throw new ConflictException("A person with this personal number already exists.");

        PersonMapper.ApplyUpdate(person, request.Data);
        await uow.SaveChangesAsync(ct);
    }
}