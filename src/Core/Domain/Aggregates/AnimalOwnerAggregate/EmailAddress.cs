namespace Application.Domain.Aggregates.AnimalOwnerAggregate;

public class EmailAddress : IValueObject
{
    public EmailAddress(string emailAddress)
    {
        // TODO: Add validation
        Email = emailAddress;
    }

    public string Email { get; private set; }
}