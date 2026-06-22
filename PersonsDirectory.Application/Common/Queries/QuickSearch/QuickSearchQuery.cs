using MediatR;
using PersonsDirectory.Application.Common.Models;
using PersonsDirectory.Application.Persons.Dtos;

namespace PersonsDirectory.Application.Common.Queries.QuickSearch;

public sealed record QuickSearchQuery(QuickSearchRequest Data)
    : IRequest<PagedResult<PersonListItemResponse>>;