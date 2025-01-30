using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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
    [Authorize(Roles = "Vet,Receptionist")]
    [SwaggerOperation(Tags = ["AnimalOwner"])]
    public async Task<ActionResult<GetAnimalOwnerResponse>> GetSingle(int id)
    {
        var animalOwner = await _dbContext.AnimalOwners.SingleOrDefaultAsync(o => o.Id == id);

        if (animalOwner == null) return NotFound();

        return Ok(GetAnimalOwnerMapper.MapAnimalOwnerToGetAnimalOwnerResponse(animalOwner));
    }

    [HttpGet]
    [Authorize(Roles = "Vet,Receptionist")]
    [SwaggerOperation(Tags = ["AnimalOwner"])]
    public async Task<ActionResult<IEnumerable<GetAnimalOwnerResponse>>> GetAll()
    {
        var animalOwners = await _dbContext.AnimalOwners.ToListAsync();

        return Ok(animalOwners.Select(GetAnimalOwnerMapper.MapAnimalOwnerToGetAnimalOwnerResponse));
    }
}