using MediatR;
using PersonsDirectory.Application.Common.Exceptions;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Mapping;
using PersonsDirectory.Application.Persons.Dtos;

namespace PersonsDirectory.Application.Common.Queries.GetPersonById;

public sealed class GetPersonByIdHandler(IUnitOfWork uow)
    : IRequestHandler<GetPersonByIdQuery, PersonDetailsResponse>
{
    public async Task<PersonDetailsResponse> Handle(GetPersonByIdQuery request, CancellationToken ct)
    {
        var person = await uow.Persons.GetFullByIdAsync(request.Id, ct)
                     ?? throw new NotFoundException("Person", request.Id);

        return PersonMapper.ToDetails(person);
    }
}