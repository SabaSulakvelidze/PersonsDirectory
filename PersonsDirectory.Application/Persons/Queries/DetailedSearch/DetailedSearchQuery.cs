using MediatR;
using PersonsDirectory.Application.Common.Models;
using PersonsDirectory.Application.Persons.Dtos;
using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Queries.DetailedSearch;

public sealed class DetailedSearchQuery
    : PagedRequest, IRequest<PagedResult<PersonListItemResponse>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
    public string? PersonalNumber { get; set; }
    public DateOnly? DateOfBirthFrom { get; set; }
    public DateOnly? DateOfBirthTo { get; set; }
    public int? CityId { get; set; }
}