using MediatR;

namespace PersonsDirectory.Application.Common.Commands.UploadImage;

public sealed record UploadImageCommand(int PersonId, Stream Content, string FileName)
    : IRequest<string>;