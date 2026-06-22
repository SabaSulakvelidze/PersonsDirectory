using PersonsDirectory.Application.Common.Models;
using PersonsDirectory.Domain.Enums;

namespace PersonsDirectory.Application.Persons.Dtos;

public sealed class DetailedSearchRequest : PagedRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
    public string? PersonalNumber { get; set; }
    public DateOnly? DateOfBirthFrom { get; set; }
    public DateOnly? DateOfBirthTo { get; set; }
    public int? CityId { get; set; }
}