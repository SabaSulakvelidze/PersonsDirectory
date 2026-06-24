using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectory.Infrastructure.Persistence.Repositories
{
    public sealed class PersonRepository(AppDbContext _context) : GenericRepository<Person>(_context), IPersonRepository
    {
        public async Task<Person?> GetFullByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Persons
                .Include(p => p.City)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.RelatedPersons)
                    .ThenInclude(r => r.RelatedPerson)
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }
    }
}
