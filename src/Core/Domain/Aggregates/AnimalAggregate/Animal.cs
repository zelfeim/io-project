using Application.Domain.Aggregates.AnimalOwnerAggregate;
using Application.Domain.Aggregates.VisitAggregate;

namespace Application.Domain.Aggregates.AnimalAggregate;

public class Animal : IEntity, IAggregateRoot
{
    public int Age;
    public string Name;
    public string Race;
    public string Species;

    public Animal(int id, AnimalOwner animalOwner, string name, string species, string race, int age)
    {
        Id = id;
        Age = age;
        Name = name;
        Race = race;
        Species = species;
    }

    public int AnimalOwnerId { get; private set; }

    public AnimalOwner AnimalOwner { get; private set; }
    public IReadOnlyCollection<Visit> Visits { get; private set; }

    public int Id { get; }
}