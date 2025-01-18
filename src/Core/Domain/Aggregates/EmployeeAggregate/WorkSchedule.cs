namespace Application.Domain.Aggregates.EmployeeAggregate;

public class WorkSchedule : IEntity
{
    public int EmployeeId { get; private set; }
    public uint ShiftDuration { get; }
    public DateTime Date { get; }
    public int Id { get; }
}