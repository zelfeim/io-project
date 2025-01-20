namespace Application.Domain.Aggregates.AnimalOwnerAggregate;

public class EmailAddress : IValueObject
{
    public EmailAddress(string email)
    {
        // TODO: Add validation
        Email = email;
    }

    public string Email { get; private set; }
}