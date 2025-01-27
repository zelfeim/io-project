namespace Application.Features.AnimalOwner.CreateAnimalOwner;

public record CreateAnimalOwnerRequest
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string Telephone { get; init; }
    public string Address { get; init; }
}