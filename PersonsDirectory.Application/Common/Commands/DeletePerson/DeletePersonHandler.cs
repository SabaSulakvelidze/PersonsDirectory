using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;

namespace PersonsDirectory.Application.Common.Commands.DeletePerson;

public sealed class DeletePersonHandler(IUnitOfWork uow, IFileStorage files) : IRequestHandler<DeletePersonCommand>
{

    public async Task Handle(DeletePersonCommand request, CancellationToken ct)
    {
        var person = await uow.Persons.GetByIdAsync(request.Id, ct)
                     ?? throw new NotFoundException("Person", request.Id);

        if (!string.IsNullOrEmpty(person.ImagePath))
            files.Delete(person.ImagePath);

        uow.Persons.Remove(person);
        await uow.SaveChangesAsync(ct);
    }
}