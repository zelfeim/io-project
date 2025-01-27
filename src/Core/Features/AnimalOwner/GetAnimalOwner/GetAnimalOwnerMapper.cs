namespace Application.Features.AnimalOwner.GetAnimalOwner;

public static class GetAnimalOwnerMapper
{
    public static GetAnimalOwnerResponse MapAnimalOwnerToGetAnimalOwnerResponse(
        Domain.Aggregates.AnimalOwnerAggregate.AnimalOwner animalOwner)
    {
        return new GetAnimalOwnerResponse
        {
            Id = animalOwner.Id,
            Name = animalOwner.Name,
            Surname = animalOwner.Surname,
            Email = animalOwner.Email,
            Address = animalOwner.Address,
            Telephone = animalOwner.Telephone
        };
    }
}