using System.Runtime.InteropServices.JavaScript;
using Application.Domain.AnimalAggregate;
using Application.Domain.EmployeeAggregate;
using Application.Features.Visit.Enums;
using Application.Features.Visits.Enums;

namespace Application.Domain.VisitAggregate;

public class Visit : IAggregateRoot
{
    public int Pk;
    public Animal Animal;
    public Employee Employee;
    public DateTime Date;
    public VisitType VisitType;
    public string VisitInformation;
    public int VisitLength;
    public VisitStatus VisitStatus;
    public string SuggestedTreatment;
}