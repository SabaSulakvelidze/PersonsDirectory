using PersonsDirectory.Application.Common.Models;

namespace PersonsDirectory.Application.Persons.Dtos;

public sealed class QuickSearchRequest : PagedRequest
{
    public string? Term { get; set; }
}