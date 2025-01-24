using Application.Infrastructure.Persistence;
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
    public async Task<ActionResult> Handle(int id, [FromBody] RescheduleVisitRequest request)
    {
        var visit = await _dbContext.Visits.SingleAsync(v => v.Id == id);

        visit.RescheduleVisit(request.Date, request.VisitLength);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}