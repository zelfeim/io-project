using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<GetVisitResponse>> Handle(int id)
    {
        var visit = await _dbContext.Visits.SingleOrDefaultAsync(v => v.Id == id);

        if (visit == null)
        {
            return NotFound();
        }

        return GetVisitMapper.MapVisitToGetVisitResponse(visit);
    }
}