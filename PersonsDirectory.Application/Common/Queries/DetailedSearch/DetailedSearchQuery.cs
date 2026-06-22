using MediatR;
using PersonsDirectory.Application.Common.Models;
using PersonsDirectory.Application.Persons.Dtos;

namespace PersonsDirectory.Application.Common.Queries.DetailedSearch;

public sealed record DetailedSearchQuery(DetailedSearchRequest Data)
    : IRequest<PagedResult<PersonListItemResponse>>;