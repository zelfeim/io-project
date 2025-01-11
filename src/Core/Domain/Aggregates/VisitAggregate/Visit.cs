using Application.Domain.Aggregates.VisitAggregate.Enums;

namespace Application.Domain.Aggregates.VisitAggregate;

public class Visit : IAggregateRoot
{
    public int Id;
    public int AnimalId;
    public int EmployeeId;
    public DateTime Date;
    public VisitType VisitType;
    public int VisitLength;
    public string VisitInformation;
    public VisitStatus VisitStatus;
    public string SuggestedTreatment;
}