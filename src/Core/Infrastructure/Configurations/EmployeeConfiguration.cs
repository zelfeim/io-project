using Application.Domain.Aggregates.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(e => e.Surname)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(e => e.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(e => e.Address)
            .IsRequired();

        builder.OwnsOne(e => e.EmailAddress);

        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasMany(e => e.WorkSchedule)
            .WithOne()
            .HasForeignKey(ws => ws.EmployeeId)
            .IsRequired();
    }
}