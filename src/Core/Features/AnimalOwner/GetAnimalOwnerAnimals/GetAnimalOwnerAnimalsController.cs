using Application.Features.Animal.GetAnimal;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Features.AnimalOwner.GetAnimalOwnerAnimals;

[ApiController]
[Route("api/animal-owner")]
[Authorize(Roles = "Admin,Vet,Receptionist")]
public class GetAnimalOwnerAnimalsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAnimalOwnerAnimalsController> _logger;

    public GetAnimalOwnerAnimalsController(ILogger<GetAnimalOwnerAnimalsController> logger,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}/animals")]
    public ActionResult<IEnumerable<GetAnimalResponse>> GetAnimalsForAnimalOwner(int id)
    {
        var animals = _dbContext.Animals.Where(a => a.AnimalOwnerId == id).ToList();

        if (animals.Count == 0) return NotFound();

        return Ok(animals.Select(GetAnimalMapper.MapAnimalToGetAnimalResponse));
    }
}