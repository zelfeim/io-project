using Application.Domain.Aggregates.AnimalOwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Configurations;

public class AnimalOwnerConfiguration : IEntityTypeConfiguration<AnimalOwner>
{
    public void Configure(EntityTypeBuilder<AnimalOwner> builder)
    {
        builder.HasKey(ao => ao.Id);

        builder.Property(ao => ao.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(ao => ao.Surname)
            .IsRequired()
            .HasMaxLength(150);

        builder.OwnsOne(ao => ao.Email);
        
        builder.OwnsOne(ao => ao.Address);

        builder.Property(ao => ao.Telephone)
            .IsRequired();
    }
}