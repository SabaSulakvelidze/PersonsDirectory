using PersonsDirectory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectory.Application.Common.Interfaces
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<Person?> GetFullByIdAsync(int id, CancellationToken ct = default);
    }
}
