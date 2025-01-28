using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AnimalOwner.GetAnimalOwner;

[ApiController]
[Route("api/animal-owner")]
[Authorize(Roles = "Admin,Vet,Receptionist")]
public class GetAnimalOwnerController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public GetAnimalOwnerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetAnimalOwnerResponse>> GetSingle(int id)
    {
        var animalOwner = await _dbContext.AnimalOwners.SingleOrDefaultAsync(o => o.Id == id);

        if (animalOwner == null) return NotFound();

        return Ok(GetAnimalOwnerMapper.MapAnimalOwnerToGetAnimalOwnerResponse(animalOwner));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAnimalOwnerResponse>>> GetAll()
    {
        var animalOwners = await _dbContext.AnimalOwners.ToListAsync();

        return Ok(animalOwners.Select(GetAnimalOwnerMapper.MapAnimalOwnerToGetAnimalOwnerResponse));
    }
}