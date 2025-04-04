using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.AnimalOwner.DeleteAnimalOwner;

[ApiController]
[Route("api/animal-owner")]
public class DeleteAnimalOwnerController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteAnimalOwnerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Receptionist")]
    [SwaggerOperation(Tags = ["AnimalOwner"])]
    public async Task<ActionResult> Handle(int id)
    {
        var animalOwner = await _dbContext.AnimalOwners.FindAsync(id);

        if (animalOwner == null) return NotFound();

        _dbContext.AnimalOwners.Remove(animalOwner);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}