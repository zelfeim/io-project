namespace Application.Features.Animal.GetAnimal;

public record GetAnimalResponse
{
    public int Id { get; init; }
    public int Age { get; init; }
    public string Name { get; init; }
    public string Race { get; init; }
    public string Species { get; init; }
    public int AnimalOwnerId { get; init; }
}