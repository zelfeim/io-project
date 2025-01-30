namespace Application.Features.Animal.CreateAnimal;

public record CreateAnimalRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Race { get; set; }
    public string Species { get; set; }
    public int AnimalOwnerId { get; set; }
}