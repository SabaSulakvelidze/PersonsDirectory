using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Infrastructure.Persistence.Configurations;

public sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> b)
    {
        b.ToTable("Cities");
        b.HasKey(c => c.Id);
        b.Property(c => c.Name).IsRequired().HasMaxLength(100);
        b.HasIndex(c => c.Name).IsUnique();
    }
}