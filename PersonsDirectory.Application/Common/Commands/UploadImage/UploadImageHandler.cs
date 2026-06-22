using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;

namespace PersonsDirectory.Application.Common.Commands.UploadImage;

public sealed class UploadImageHandler(IUnitOfWork uow, IFileStorage files) : IRequestHandler<UploadImageCommand, string>
{

    public async Task<string> Handle(UploadImageCommand request, CancellationToken ct)
    {
        var person = await uow.Persons.GetByIdAsync(request.PersonId, ct)
                     ?? throw new NotFoundException("Person", request.PersonId);

        // Remove the previous file if replacing.
        if (!string.IsNullOrEmpty(person.ImagePath))
            files.Delete(person.ImagePath);

        var relativePath = await files.SaveAsync(request.Content, request.FileName, ct);
        person.ImagePath = relativePath;
        await uow.SaveChangesAsync(ct);

        return relativePath;
    }
}