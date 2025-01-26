using Application.Domain.Aggregates.VisitAggregate.Enums;

namespace Application.Features.Visit.CreateVisit;

public class CreateVisitRequest
{
    public int AnimalId { get; set; }
    public DateTime Date { get; set; }
    public int EmployeeId { get; set; }
    public VisitType Type { get; set; }
    public string VisitInformation { get; set; } = "";
    public int VisitLength { get; set; }
}