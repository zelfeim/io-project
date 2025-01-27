using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Visit.CancelVisit;

[ApiController]
[Route("api/visit")]
[Authorize(Roles = "Receptionist")]
public class CancelVisitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CancelVisitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut("{id:int}/cancel")]
    public async Task<ActionResult> Handle(int id)
    {
        var visit = await _dbContext.Visits.SingleOrDefaultAsync(v => v.Id == id);

        if (visit == null) return NotFound();

        visit.SetCancelledStatus();
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}