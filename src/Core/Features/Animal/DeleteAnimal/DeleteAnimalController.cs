using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Animal.DeleteAnimal;

[ApiController]
[Route("api/animal")]
public class DeleteAnimalController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteAnimalController> _logger;

    public DeleteAnimalController(ILogger<DeleteAnimalController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Receptionist")]
    [SwaggerOperation(Tags = ["Animal"])]
    public async Task<ActionResult> Handle(int id)
    {
        var animal = await _dbContext.Animals.FindAsync(id);

        if (animal == null) return NotFound();

        _dbContext.Animals.Remove(animal);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}