using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Features.Animal.CreateAnimal;

[ApiController]
[Route("api/animal")]
public class CreateAnimalController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CreateAnimalController> _logger;

    public CreateAnimalController(ILogger<CreateAnimalController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost]
    [Authorize(Roles = "Receptionist")]
    public async Task<ActionResult<int>> Handle([FromBody] CreateAnimalRequest request)
    {
        var animal = new Domain.Aggregates.AnimalAggregate.Animal(request.AnimalOwnerId, request.Name, request.Species,
            request.Race, request.Age);

        _dbContext.Animals.Add(animal);
        await _dbContext.SaveChangesAsync();

        return animal.Id;
    }
}