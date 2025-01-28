using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<GetAnimalResponse>> GetSingle(int id)
    {
        var animal = await _dbContext.Animals.FindAsync(id);

        if (animal == null) return NotFound();

        return Ok(GetAnimalMapper.MapAnimalToGetAnimalResponse(animal));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAnimalResponse>>> GetAll()
    {
        var animals = await _dbContext.Animals.ToListAsync();

        if (animals.Count == 0) return NotFound();

        return Ok(animals.Select(GetAnimalMapper.MapAnimalToGetAnimalResponse));
    }
}