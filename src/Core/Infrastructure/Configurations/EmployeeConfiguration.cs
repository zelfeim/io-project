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
            .IsRequired();

        builder.OwnsOne(e => e.Address);

        builder.HasMany(e => e.WorkSchedule)
            .WithOne()
            .HasForeignKey(ws => ws.EmployeeId)
            .IsRequired();
    }
}