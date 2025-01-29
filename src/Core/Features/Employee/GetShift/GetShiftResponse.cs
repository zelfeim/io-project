namespace Application.Features.Employee.GetShift;

public record GetShiftResponse
{
    public int Id { get; init; }
    public DateOnly Date { get; init; }
    public TimeOnly ShiftStart { get; init; }
    public TimeOnly ShiftEnd { get; init; }
}