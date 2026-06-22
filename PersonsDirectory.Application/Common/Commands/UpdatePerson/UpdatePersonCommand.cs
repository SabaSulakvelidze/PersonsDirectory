using MediatR;
using PersonsDirectory.Application.DTOs.Requests;

namespace PersonsDirectory.Application.Common.Commands.UpdatePerson;

public sealed record UpdatePersonCommand(int Id, UpdatePersonRequest Data) : IRequest;