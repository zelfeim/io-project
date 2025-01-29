namespace Application.Features.Employee.NewShift;

public record NewShiftRequest
{
    public DateOnly Date { get; init; }
    public TimeOnly ShiftStart { get; init; }
    public TimeOnly ShiftEnd { get; init; }
}