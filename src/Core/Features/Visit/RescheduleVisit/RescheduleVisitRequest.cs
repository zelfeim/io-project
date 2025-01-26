namespace Application.Features.Visit.RescheduleVisit;

public record RescheduleVisitRequest
{
    public DateTime Date { get; set; }
    public int VisitLength { get; set; }
}