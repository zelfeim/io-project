using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AnimalOwner.GetAnimalOwner;

[ApiController]
[Route("api/animal-owner")]
public class GetAnimalOwnerController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public GetAnimalOwnerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetAnimalOwnerResponse>> Handle(int id)
    {
        var animalOwner = await _dbContext.AnimalOwners.SingleOrDefaultAsync(o => o.Id == id);

        if (animalOwner == null) return NotFound();

        return Ok(GetAnimalOwnerMapper.MapAnimalOwnerToGetAnimalOwnerResponse(animalOwner));
    }
}