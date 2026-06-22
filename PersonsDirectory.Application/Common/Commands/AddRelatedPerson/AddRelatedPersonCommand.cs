using MediatR;
using PersonsDirectory.Application.DTOs.Requests;

namespace PersonsDirectory.Application.Common.Commands.AddRelatedPerson;

public sealed record AddRelatedPersonCommand(int PersonId, AddRelatedPersonRequest Data)
    : IRequest;