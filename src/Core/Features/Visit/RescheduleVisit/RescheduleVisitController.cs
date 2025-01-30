using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Visit.RescheduleVisit;

public class RescheduleVisitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public RescheduleVisitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut("visit/{id:int}/reschedule")]
    [Authorize(Roles = "Receptionist")]
    public async Task<ActionResult> Handle(int id, [FromBody] RescheduleVisitRequest request)
    {
        var visit = await _dbContext.Visits.SingleOrDefaultAsync(v => v.Id == id);

        if (visit == null) return NotFound();

        visit.RescheduleVisit(request.Date, request.VisitLength);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}