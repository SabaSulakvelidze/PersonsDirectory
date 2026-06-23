using MediatR;
using PersonsDirectory.Application.Common.Models;
using PersonsDirectory.Application.Persons.Dtos;

namespace PersonsDirectory.Application.Persons.Queries.QuickSearch;

public sealed class QuickSearchQuery
    : PagedRequest, IRequest<PagedResult<PersonListItemResponse>>
{
    public string? Term { get; set; }
}