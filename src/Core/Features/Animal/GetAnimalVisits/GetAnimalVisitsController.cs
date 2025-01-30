using Application.Features.Visit.GetVisit;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Animal.GetAnimalVisits;

[ApiController]
[Route("api/animal")]
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
    [Authorize(Roles = "Vet,Receptionist")]
    [SwaggerOperation(Tags = ["Animal"])]
    public ActionResult<IEnumerable<GetVisitResponse>> GetAnimalVisits([FromRoute] int id)
    {
        var visits = _dbContext.Visits.Where(v => v.AnimalId == id).ToList();

        return Ok(visits.Select(GetVisitMapper.MapVisitToGetVisitResponse));
    }
}