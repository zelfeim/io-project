using Application.Domain.Aggregates.VisitAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Configurations;

public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.AnimalId)
            .IsRequired();

        builder.Property(v => v.EmployeeId)
            .IsRequired();

        builder.Property(v => v.Date)
            .IsRequired();

        builder.Property(v => v.VisitType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(v => v.VisitLength)
            .IsRequired();

        builder.Property(v => v.VisitInformation)
            .HasMaxLength(500);

        builder.Property(v => v.VisitStatus)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.OwnsOne(v => v.Prescription);

        builder.Property(v => v.SuggestedTreatment)
            .HasMaxLength(500);

        builder.HasOne(v => v.Animal)
            .WithMany(a => a.Visits)
            .HasForeignKey(v => v.AnimalId)
            .IsRequired();

        builder.HasOne(v => v.Employee)
            .WithMany()
            .HasForeignKey(v => v.EmployeeId)
            .IsRequired();
    }
}