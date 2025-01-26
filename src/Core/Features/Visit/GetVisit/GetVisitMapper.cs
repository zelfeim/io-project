namespace Application.Features.Visit.GetVisit;

public static class GetVisitMapper
{
    public static GetVisitResponse MapVisitToGetVisitResponse(Domain.Aggregates.VisitAggregate.Visit visit)
    {
        return new GetVisitResponse()
        {
            AnimalId = visit.AnimalId,
            EmployeeId = visit.EmployeeId,
            Date = visit.Date,
            VisitType = visit.VisitType,
            VisitStatus = visit.VisitStatus,
            Prescription = visit.Prescription,
            SuggestedTreatment = visit.SuggestedTreatment,
            VisitInformation = visit.VisitInformation,
            VisitLength = visit.VisitLength,
        };
    }
}