using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;

namespace PersonsDirectory.Application.Persons.Commands.DeletePerson;

public sealed class DeletePersonHandler(IUnitOfWork _uow, IFileStorage _files) : IRequestHandler<DeletePersonCommand>
{
   

    public async Task Handle(DeletePersonCommand request, CancellationToken ct)
    {
        var person = await _uow.Persons.GetByIdAsync(request.Id, ct)
                     ?? throw new NotFoundException("Person", request.Id);

        if (!string.IsNullOrEmpty(person.ImagePath))
            _files.Delete(person.ImagePath);

        _uow.Persons.Remove(person);
        await _uow.SaveChangesAsync(ct);
    }
}