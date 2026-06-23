using MediatR;

namespace PersonsDirectory.Application.Persons.Commands.DeletePerson;

public sealed record DeletePersonCommand(int Id) : IRequest;