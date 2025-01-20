using Application.Domain.Aggregates.AnimalAggregate;
using Application.Domain.Shared;

namespace Application.Domain.Aggregates.AnimalOwnerAggregate;

public class AnimalOwner : IEntity
{
    public Address Address;
    public EmailAddress Email;
    public string Name;
    public string Surname;
    public string Telephone;

    private AnimalOwner()
    {
    }

    public AnimalOwner(string name, string surname, string email, Address address, string telephone)
    {
        Name = name;
        Surname = surname;
        Email = new EmailAddress(email);
        Address = address;
        Telephone = telephone;
    }

    public List<int>? AnimalIds { get; }
    // Czy chcemy tworzyć właściciela wraz ze zwięrzętami?

    public IReadOnlyCollection<Animal> Animals { get; private set; }

    public int Id { get; }
}