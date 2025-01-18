using Application.Domain.Aggregates.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Configurations;

public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
{
    public void Configure(EntityTypeBuilder<WorkSchedule> builder)
    {
        builder.HasKey(ws => ws.Id);

        builder.Property(ws => ws.ShiftDuration)
            .IsRequired();

        builder.Property(ws => ws.Date)
            .IsRequired();

        builder.Property(ws => ws.EmployeeId)
            .IsRequired();
    }
}