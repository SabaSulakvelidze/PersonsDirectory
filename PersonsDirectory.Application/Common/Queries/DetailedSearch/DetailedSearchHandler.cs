using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Common.Models;
using PersonsDirectory.Application.Common.Queries.DetailedSearch;
using PersonsDirectory.Application.Common.Queries.QuickSearch;
using PersonsDirectory.Application.Persons.Dtos;

namespace PersonsDirectory.Application.Persons.Queries.DetailedSearch;

public sealed class DetailedSearchHandler(IUnitOfWork uow)
    : IRequestHandler<DetailedSearchQuery, PagedResult<PersonListItemResponse>>
{
    public async Task<PagedResult<PersonListItemResponse>> Handle(
        DetailedSearchQuery request, CancellationToken ct)
    {
        var r = request.Data;
        var query = uow.Persons.Query().Include(p => p.City).AsQueryable();

        if (!string.IsNullOrWhiteSpace(r.FirstName))
            query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{r.FirstName.Trim()}%"));
        if (!string.IsNullOrWhiteSpace(r.LastName))
            query = query.Where(p => EF.Functions.Like(p.LastName, $"%{r.LastName.Trim()}%"));
        if (!string.IsNullOrWhiteSpace(r.PersonalNumber))
            query = query.Where(p => EF.Functions.Like(p.PersonalNumber, $"%{r.PersonalNumber.Trim()}%"));
        if (r.Gender is not null)
            query = query.Where(p => p.Gender == r.Gender);
        if (r.CityId is not null)
            query = query.Where(p => p.CityId == r.CityId);
        if (r.DateOfBirthFrom is not null)
            query = query.Where(p => p.DateOfBirth >= r.DateOfBirthFrom);
        if (r.DateOfBirthTo is not null)
            query = query.Where(p => p.DateOfBirth <= r.DateOfBirthTo);

        return await QuickSearchHandler.Paginate(query, r.Page, r.PageSize, ct);
    }
}