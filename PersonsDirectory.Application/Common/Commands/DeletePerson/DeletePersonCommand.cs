using MediatR;

namespace PersonsDirectory.Application.Common.Commands.DeletePerson;

public sealed record DeletePersonCommand(int Id) : IRequest;