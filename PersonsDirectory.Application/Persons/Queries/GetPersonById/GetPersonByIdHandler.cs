using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Persons.Dtos;
using PersonsDirectory.Application.Persons.Mapping;

namespace PersonsDirectory.Application.Persons.Queries.GetPersonById;

public sealed class GetPersonByIdHandler(IUnitOfWork _uow)
    : IRequestHandler<GetPersonByIdQuery, PersonDetailsResponse>
{
    public async Task<PersonDetailsResponse> Handle(GetPersonByIdQuery request, CancellationToken ct)
    {
        var person = await _uow.Persons.GetFullByIdAsync(request.Id, ct)
                     ?? throw new NotFoundException("Person", request.Id);

        return PersonMapper.ToDetails(person);
    }
}