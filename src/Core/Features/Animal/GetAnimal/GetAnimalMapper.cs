namespace Application.Features.Animal.GetAnimal;

public static class GetAnimalMapper
{
    public static GetAnimalResponse MapAnimalToGetAnimalResponse(
        Domain.Aggregates.AnimalAggregate.Animal animal)
    {
        return new GetAnimalResponse
        {
            Id = animal.Id,
            AnimalOwnerId = animal.AnimalOwnerId,
            Age = animal.Age,
            Name = animal.Name,
            Race = animal.Race,
            Species = animal.Species
        };
    }
}