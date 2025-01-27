using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Visit.EndVisit;

[ApiController]
[Route("api/visit")]
[Authorize(Roles = "Vet")]
public class EndVisitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public EndVisitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("{id:int}/end")]
    public async Task<ActionResult> Handle(int id, [FromBody] EndVisitRequest request)
    {
        var visit = await _dbContext.Visits.SingleOrDefaultAsync(v => v.Id == id);

        if (visit == null) return NotFound();

        visit.EndVisit(request.SuggestedTreatment, request.PrescribedMeds);
        await _dbContext.SaveChangesAsync();

        var v = await _dbContext.Visits.SingleOrDefaultAsync(v => v.Id == id);

        return Ok();
    }
}