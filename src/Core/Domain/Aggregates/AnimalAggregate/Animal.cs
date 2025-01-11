using Application.Domain.Aggregates.AnimalOwnerAggregate;

namespace Application.Domain.Aggregates.AnimalAggregate;

public class Animal : IEntity, IAggregateRoot
{
    public Animal(int id, AnimalOwner animalOwner, string name, string species, string race, int age)
    {
        Age = age;
        Name = name;
        Race = race;
        Species = species;
        Id = id;
    }
    
    public int Id { get; }
    public int AnimalOwnerId { get; private set; }
    public int Age;
    public string Name;
    public string Race;
    public string Species;


}