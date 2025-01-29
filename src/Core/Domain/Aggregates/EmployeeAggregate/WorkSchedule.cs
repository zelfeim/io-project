namespace Application.Domain.Aggregates.EmployeeAggregate;

public class WorkSchedule : IEntity
{
    private WorkSchedule()
    {
    }

    public WorkSchedule(DateOnly date, TimeOnly shiftStart, TimeOnly shiftEnd)
    {
        Date = date;
        ShiftStart = shiftStart;
        ShiftEnd = shiftEnd;
    }

    public int EmployeeId { get; private set; }
    public DateOnly Date { get; }
    public TimeOnly ShiftStart { get; }
    public TimeOnly ShiftEnd { get; }
    public int Id { get; }
}