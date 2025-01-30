using Application.Domain.Aggregates.VisitAggregate;
using Application.Domain.Aggregates.VisitAggregate.Enums;

namespace Application.Features.Visit.GetVisit;

public record GetVisitResponse
{
    public int Id { get; init; }
    public int AnimalId { get; init; }
    public int EmployeeId { get; init; }
    public DateTime Date { get; init; }
    public uint VisitLength { get; init; }
    public VisitType VisitType { get; init; }
    public VisitStatus VisitStatus { get; init; }
    public string VisitInformation { get; init; } = "";
    public string? SuggestedTreatment { get; init; }
    public Prescription? Prescription { get; init; }
}