using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Visit.GetVisit;

public class GetVisitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public GetVisitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("visit/{id:int}")]
    public async Task<ActionResult<Domain.Aggregates.VisitAggregate.Visit>> Handle(int id)
    {
        var visit = await _dbContext.Visits.SingleAsync(v => v.Id == id);

        return visit;
    }
}