namespace Application.Features.Animal.UpdateAnimal;

public record UpdateAnimalRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Race { get; set; }
    public string Species { get; set; }
}