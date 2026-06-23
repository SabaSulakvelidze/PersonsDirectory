using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Application.Common.Models;
using PersonsDirectory.Application.Persons.Dtos;
using PersonsDirectory.Application.Persons.Mapping;

namespace PersonsDirectory.Application.Persons.Queries.QuickSearch;

public sealed class QuickSearchHandler(IUnitOfWork _uow)
    : IRequestHandler<QuickSearchQuery, PagedResult<PersonListItemResponse>>
{
    public async Task<PagedResult<PersonListItemResponse>> Handle(
        QuickSearchQuery request, CancellationToken ct)
    {
        var r = request;
        var query = _uow.Persons.Query().Include(p => p.City).AsQueryable();

        if (!string.IsNullOrWhiteSpace(r.Term))
        {
            var term = r.Term.Trim();
            // EF.Functions.Like => SQL LIKE, as the task requires.
            query = query.Where(p =>
                EF.Functions.Like(p.FirstName, $"%{term}%") ||
                EF.Functions.Like(p.LastName, $"%{term}%") ||
                EF.Functions.Like(p.PersonalNumber, $"%{term}%"));
        }

        return await Paginate(query, r.Page, r.PageSize, ct);
    }

    internal static async Task<PagedResult<PersonListItemResponse>> Paginate(
        IQueryable<Domain.Entities.Person> query, int page, int pageSize, CancellationToken ct)
    {
        var total = await query.CountAsync(ct);
        var items = await query
            .OrderBy(p => p.LastName).ThenBy(p => p.FirstName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<PersonListItemResponse>(
            items.Select(PersonMapper.ToListItem).ToList(), total, page, pageSize);
    }
}