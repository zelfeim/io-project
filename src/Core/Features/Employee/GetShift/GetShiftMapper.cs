using Application.Domain.Aggregates.EmployeeAggregate;

namespace Application.Features.Employee.GetShift;

public static class GetShiftMapper
{
    public static GetShiftResponse MapWorkScheduleToGetShiftResponse(WorkSchedule workSchedule)
    {
        return new GetShiftResponse
        {
            Id = workSchedule.Id,
            Date = workSchedule.Date,
            ShiftStart = workSchedule.ShiftStart,
            ShiftEnd = workSchedule.ShiftEnd
        };
    }
}