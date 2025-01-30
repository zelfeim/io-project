using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.AnimalOwner.CreateAnimalOwner;

[ApiController]
[Route("api/animal-owner")]
public class CreateAnimalOwnerController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CreateAnimalOwnerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Authorize(Roles = "Receptionist")]
    public async Task<ActionResult<int>> Handle([FromBody] CreateAnimalOwnerRequest request)
    {
        var animalOwner = new Domain.Aggregates.AnimalOwnerAggregate.AnimalOwner(request.Name, request.Surname,
            request.Email, request.Address, request.Telephone);

        _dbContext.AnimalOwners.Add(animalOwner);
        await _dbContext.SaveChangesAsync();

        return Created(string.Empty, animalOwner.Id);
    }
}