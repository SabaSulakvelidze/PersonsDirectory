using MediatR;
using PersonsDirectory.Application.Persons.Dtos;

namespace PersonsDirectory.Application.Common.Queries.GetPersonById;

public sealed record GetPersonByIdQuery(int Id) : IRequest<PersonDetailsResponse>;