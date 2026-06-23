using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Infrastructure.Persistence.Configurations
{
    public sealed class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> b)
        {
            b.ToTable("PhoneNumbers");
            b.HasKey(ph => ph.Id);
            b.Property(p => p.Number).IsRequired().HasMaxLength(50);
            b.Property(p => p.Type).HasConversion<int>();
        }
    }
}
