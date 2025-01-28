using Application.Features.Animal.GetAnimal;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Features.Animal.GetAnimalVisits;

[ApiController]
[Route("api/animal")]
[Authorize(Roles = "Admin,Vet,Receptionist")]
public class GetAnimalVisitsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAnimalVisitsController> _logger;

    public GetAnimalVisitsController(ILogger<GetAnimalVisitsController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}/visits")]
    public ActionResult<IEnumerable<GetAnimalResponse>> GetAnimalVisits([FromRoute] int id)
    {
        var animals = _dbContext.Animals.Where(a => a.Id == id).ToList();

        if (animals.Count == 0) return NotFound();

        return Ok(animals.Select(GetAnimalMapper.MapAnimalToGetAnimalResponse));
    }
}