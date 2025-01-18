using Application.Domain.Aggregates.AnimalAggregate;
using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Domain.Aggregates.VisitAggregate.Enums;

namespace Application.Domain.Aggregates.VisitAggregate;

public class Visit : IAggregateRoot
{
    public int AnimalId;
    public DateTime Date;
    public int EmployeeId;
    public int Id;
    public Prescription? Prescription;
    public string? SuggestedTreatment;
    public string? VisitInformation;
    public uint VisitLength;
    public VisitStatus VisitStatus;
    public VisitType VisitType;

    public Visit(int animalId, int employeeId, DateTime date, VisitType visitType, uint visitLength,
        string? visitInformation = null)
    {
        AnimalId = animalId;
        EmployeeId = employeeId;
        Date = date;
        VisitType = visitType;
        VisitLength = visitLength;
        VisitInformation = visitInformation;
        VisitStatus = VisitStatus.Planned;
    }

    public Visit(int animalId, int employeeId, DateTime date, VisitType visitType, int visitLength,
        string? visitInformation = null)
    {
        if (visitLength < 0) throw new ArgumentException("Visit length must be greater than zero", nameof(visitLength));

        AnimalId = animalId;
        EmployeeId = employeeId;
        Date = date;
        VisitType = visitType;
        VisitLength = (uint)visitLength;
        VisitInformation = visitInformation;
        VisitStatus = VisitStatus.Planned;
    }

    public Animal Animal { get; }

    public Employee Employee { get; }

    public void SetCompletedStatus()
    {
        if (VisitStatus != VisitStatus.Planned)
            throw new Exception(
                $"It's not possible to change visit status from {VisitStatus} to {VisitStatus.Completed}.");

        VisitStatus = VisitStatus.Completed;
    }

    public void SetCancelledStatus()
    {
        if (VisitStatus != VisitStatus.Planned)
            throw new Exception(
                $"It's not possible to change visit status from {VisitStatus} to {VisitStatus.Canceled}.");

        VisitStatus = VisitStatus.Canceled;
    }
}