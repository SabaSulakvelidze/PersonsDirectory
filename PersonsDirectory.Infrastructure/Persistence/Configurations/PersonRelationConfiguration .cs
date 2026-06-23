using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Infrastructure.Persistence.Configurations;

public sealed class PersonRelationConfiguration : IEntityTypeConfiguration<PersonRelation>
{
    public void Configure(EntityTypeBuilder<PersonRelation> b)
    {
        b.ToTable("PersonRelations");
        b.HasKey(r => r.Id);
        b.Property(r => r.RelationType).HasConversion<int>();

        b.HasOne(r => r.RelatedPerson)
            .WithMany()
            .HasForeignKey(r => r.RelatedPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(r => new { r.PersonId, r.RelatedPersonId }).IsUnique();
    }
}