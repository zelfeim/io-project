using Application.Domain.Aggregates.AnimalAggregate;
using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Domain.Aggregates.VisitAggregate.Enums;

namespace Application.Domain.Aggregates.VisitAggregate;

public class Visit : IAggregateRoot
{
    private Visit()
    {
    }

    public Visit(int animalId, int employeeId, DateTime date, VisitType visitType, int visitLength,
        string visitInformation = "")
    {
        AnimalId = animalId;
        EmployeeId = employeeId;
        SetDate(date);
        VisitType = visitType;
        SetVisitLength(visitLength);
        VisitInformation = visitInformation;
        VisitStatus = VisitStatus.Planned;
    }

    public int AnimalId { get; private set; }
    public DateTime Date { get; private set; }
    public int EmployeeId { get; private set; }
    public int Id { get; private set; }
    public Prescription? Prescription { get; private set; }
    public string SuggestedTreatment { get; private set; }
    public string VisitInformation { get; private set; }
    public uint VisitLength { get; private set; }
    public VisitStatus VisitStatus { get; private set; }
    public VisitType VisitType { get; private set; }

    public Animal Animal { get; }

    public Employee Employee { get; }

    private void SetDate(DateTime date)
    {
        if (date < DateTime.Now) throw new ArgumentException("Visit date must be in the future", nameof(date));

        Date = date;
    }

    private void SetVisitLength(int visitLength)
    {
        if (visitLength <= 0) throw new ArgumentException("Visit length must be greater than zero", nameof(visitLength));

        VisitLength = (uint)visitLength;
    }

    public void SetCompletedStatus()
    {
        if (VisitStatus != VisitStatus.Planned)
            throw new Exception( // TODO: Custom exception
                $"It's not possible to change visit status from {VisitStatus} to {VisitStatus.Completed}.");

        VisitStatus = VisitStatus.Completed;
    }

    public void SetCancelledStatus()
    {
        if (VisitStatus != VisitStatus.Planned)
            throw new Exception(
                $"It's not possible to change visit status from {VisitStatus} to {VisitStatus.Cancelled}.");

        VisitStatus = VisitStatus.Cancelled;
    }

    public void RescheduleVisit(DateTime newDate, int newVisitLength)
    {
        if (VisitStatus != VisitStatus.Planned) throw new ArgumentException("Visit status must be Planned.");

        SetDate(newDate);
        SetVisitLength(newVisitLength);
    }
}