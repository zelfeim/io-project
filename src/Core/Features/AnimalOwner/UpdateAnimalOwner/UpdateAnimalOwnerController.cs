using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AnimalOwner.UpdateAnimalOwner;

[ApiController]
[Route("api/animal-owner")]
public class UpdateAnimalOwnerController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateAnimalOwnerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut("{id:int}/update")]
    [Authorize(Roles = "Receptionist")]
    public async Task<ActionResult> Handle(int id, [FromBody] UpdateAnimalOwnerRequest request)
    {
        var animalOwner = await _dbContext.AnimalOwners.SingleOrDefaultAsync(o => o.Id == id);

        if (animalOwner == null) return NotFound();

        animalOwner.Update(request.Address, request.Email, request.Telephone);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}