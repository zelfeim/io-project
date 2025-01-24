using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Visit.CancelVisit;

public class CancelVisitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CancelVisitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPut("visit/{id:int}/cancel")]
    public async Task<ActionResult> Handle(int id)
    {
        var visit = await _dbContext.Visits.SingleAsync(v => v.Id == id);
        visit.SetCancelledStatus();
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}