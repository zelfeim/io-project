using Application.Domain.Aggregates.VisitAggregate.Enums;

namespace Application.Features.Visit.CreateVisit;

public class VisitRequest
{
    public int AnimalId;
    public DateTime Date;
    public int EmployeeId;
    public VisitType Type;
    public string VisitInformation = "";
    public int VisitLength;
}