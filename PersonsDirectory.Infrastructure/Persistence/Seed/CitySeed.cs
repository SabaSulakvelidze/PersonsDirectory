using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Infrastructure.Persistence.Seed
{
    public static class CitySeed
    {
        public static void Apply(ModelBuilder mb)
        {
            mb.Entity<City>().HasData(
                new { Id = 1, Name = "Tbilisi" },
                new { Id = 2, Name = "Batumi" },
                new { Id = 3, Name = "Kutaisi" },
                new { Id = 4, Name = "Rustavi" },
                new { Id = 5, Name = "Zugdidi" }
                );
        }
    }
}
