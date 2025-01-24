namespace Application.Domain.Aggregates.VisitAggregate;

public class Prescription : IValueObject
{
    public Prescription(string prescribedMeds)
    {
        PrescribedMeds = prescribedMeds;
    }

    public string PrescribedMeds { get; private set; }
}