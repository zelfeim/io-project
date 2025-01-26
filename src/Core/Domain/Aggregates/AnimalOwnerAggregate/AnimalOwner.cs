using Application.Domain.Aggregates.AnimalAggregate;

namespace Application.Domain.Aggregates.AnimalOwnerAggregate;

public class AnimalOwner : IEntity
{
    public string Address;
    public EmailAddress Email;
    public string Name;
    public string Surname;
    public string Telephone;

    private AnimalOwner()
    {
    }

    public AnimalOwner(string name, string surname, string email, string address, string telephone)
    {
        Name = name;
        Surname = surname;
        Email = new EmailAddress(email);
        Address = address;
        Telephone = telephone;
    }

    public List<int>? AnimalIds { get; }

    public IReadOnlyCollection<Animal> Animals { get; private set; }

    public int Id { get; }

    public void Update(string? address, string? email, string? telephone)
    {
        Address = string.IsNullOrEmpty(address) ? Address : address;
        Email = string.IsNullOrEmpty(email) ? Email : new EmailAddress(email);
        Telephone = string.IsNullOrEmpty(telephone) ? Telephone : telephone;
    }
}