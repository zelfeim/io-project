using Application.Domain.Aggregates.AnimalOwnerAggregate;

namespace Application.Features.AnimalOwner.GetAnimalOwner;

public record GetAnimalOwnerResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public EmailAddress Email { get; init; }
    public string Telephone { get; init; }
    public string Address { get; init; }
}