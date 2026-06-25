using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Infrastructure.Persistence.Configurations
{
    public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> b)
        {
            b.ToTable("Persons");
            b.HasKey(p => p.Id);

            b.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            b.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            b.Property(p => p.PersonalNumber).IsRequired().HasMaxLength(11);
            b.Property(p => p.Gender).IsRequired().HasConversion<int>();
            b.Property(p => p.DateOfBirth).IsRequired().HasColumnType("date");
            b.Property(p => p.ImagePath).HasMaxLength(500);

            b.HasIndex(p => p.PersonalNumber).IsUnique();

            b.HasOne(p => p.City)
                .WithMany(c => c.Persons)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(p => p.PhoneNumbers)
                .WithOne(ph => ph.Person)
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(p => p.RelatedPersons)
                .WithOne(rp => rp.Person)
                .HasForeignKey(rp => rp.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
