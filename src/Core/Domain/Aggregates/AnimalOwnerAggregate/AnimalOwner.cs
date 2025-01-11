using Application.Domain.Shared;

namespace Application.Domain.Aggregates.AnimalOwnerAggregate;

public class AnimalOwner : IEntity
{
    public AnimalOwner(int id, List<int>? animalIds, string name, string surname, string email, Address address, string telephone)
    {
        Id = id;
        AnimalIds = animalIds;
        Name = name;
        Surname = surname;
        Email = new EmailAddress(email);
        Address = address;
        Telephone = telephone;
    }

    public int Id { get; }
    public List<int>? AnimalIds { get; }
    public Address Address;
    public EmailAddress Email;
    public string Name;
    public string Surname;
    public string Telephone;
}