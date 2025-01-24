using Application.Domain.Aggregates.AnimalAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Configurations;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AnimalOwnerId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Species)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Race)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Age);

        builder.HasOne(a => a.AnimalOwner)
            .WithMany(o => o.Animals)
            .HasForeignKey(a => a.AnimalOwnerId)
            .IsRequired();
    }
}