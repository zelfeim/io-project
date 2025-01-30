using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Visit.GetVisit;

[ApiController]
[Route("api/visit")]
public class GetVisitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public GetVisitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Vet,Receptionist")]
    [SwaggerOperation(Tags = ["Visit"])]
    public async Task<ActionResult<GetVisitResponse>> GetSingle(int id)
    {
        var visit = await _dbContext.Visits.SingleOrDefaultAsync(v => v.Id == id);

        if (visit == null) return NotFound();

        return Ok(GetVisitMapper.MapVisitToGetVisitResponse(visit));
    }

    [HttpGet]
    [Authorize(Roles = "Vet,Receptionist")]
    [SwaggerOperation(Tags = ["Visit"])]
    public async Task<ActionResult<IEnumerable<GetVisitResponse>>> GetAll()
    {
        var visits = await _dbContext.Visits.ToListAsync();

        return Ok(visits.Select(GetVisitMapper.MapVisitToGetVisitResponse));
    }
}