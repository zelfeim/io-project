namespace Application.Domain.Aggregates.EmployeeAggregate;

public class WorkSchedule : IEntity
{
    public int EmployeeId { get; private set; }
    public DateOnly Date { get; }
    public TimeOnly ShiftStart { get; }
    public TimeOnly ShiftEnd { get; }
    public int Id { get; }
}