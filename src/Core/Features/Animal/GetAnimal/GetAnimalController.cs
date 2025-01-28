using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Features.Animal.GetAnimal;

[ApiController]
[Route("api/animal")]
[Authorize(Roles = "Admin,Vet,Receptionist")]
public class GetAnimalController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAnimalController> _logger;

    public GetAnimalController(ILogger<GetAnimalController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetAnimalResponse>> Handle(int id)
    {
        var animal = await _dbContext.Animals.FindAsync(id);

        if (animal == null) return NotFound();

        return Ok(GetAnimalMapper.MapAnimalToGetAnimalResponse(animal));
    }

    [HttpGet]
    public ActionResult<List<GetAnimalResponse>> Handle()
    {
        var animals = _dbContext.Animals.ToList();

        if (animals.Count == 0) return NotFound();

        return Ok(animals.Select(GetAnimalMapper.MapAnimalToGetAnimalResponse).ToList());
    }
}

public record GetAnimalResponse
{
    public int Id { get; init; }
    public int Age { get; init; }
    public string Name { get; init; }
    public string Race { get; init; }
    public string Species { get; init; }
    public int AnimalOwnerId { get; init; }
}

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