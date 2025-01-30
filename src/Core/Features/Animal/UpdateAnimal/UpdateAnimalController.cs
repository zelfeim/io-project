using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Animal.UpdateAnimal;

[ApiController]
[Route("api/animal")]
public class UpdateAnimalController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateAnimalController> _logger;

    public UpdateAnimalController(ILogger<UpdateAnimalController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Vet,Receptionist")]
    [SwaggerOperation(Tags = ["Animal"])]
    public async Task<ActionResult> Handle(int id, [FromBody] UpdateAnimalRequest request)
    {
        var animal = await _dbContext.Animals.FindAsync(id);

        if (animal == null) return NotFound();

        animal.Name = request.Name;
        animal.Age = request.Age;
        animal.Race = request.Race;
        animal.Species = request.Species;

        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}