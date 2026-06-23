using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;

namespace PersonsDirectory.Application.Persons.Commands.UploadImage;

public sealed class UploadImageHandler(IUnitOfWork _uow, IFileStorage _files) : IRequestHandler<UploadImageCommand, string>
{
    public async Task<string> Handle(UploadImageCommand request, CancellationToken ct)
    {
        var person = await _uow.Persons.GetByIdAsync(request.PersonId, ct)
                     ?? throw new NotFoundException("Person", request.PersonId);

        // Remove the previous file if replacing.
        if (!string.IsNullOrEmpty(person.ImagePath))
            _files.Delete(person.ImagePath);

        var relativePath = await _files.SaveAsync(request.Content, request.FileName, ct);
        person.ImagePath = relativePath;
        await _uow.SaveChangesAsync(ct);

        return relativePath;
    }
}