using MediatR;
using PersonsDirectory.Application.DTOs.Requests;

namespace PersonsDirectory.Application.Common.Commands.CreatePerson;

public sealed record CreatePersonCommand(CreatePersonRequest Data) : IRequest<int>;