namespace Application.Features.AnimalOwner.UpdateAnimalOwner;

public record UpdateAnimalOwnerRequest
{
    public string? Address { get; init; }
    public string? Email { get; init; }
    public string? Telephone { get; init; }
}