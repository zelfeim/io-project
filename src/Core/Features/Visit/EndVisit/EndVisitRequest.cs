namespace Application.Features.Visit.EndVisit;

public record EndVisitRequest
{
    public string SuggestedTreatment { get; set; } = string.Empty;
    public string PrescribedMeds { get; set; } = string.Empty;
}