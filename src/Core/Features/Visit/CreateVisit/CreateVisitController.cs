using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Visit.CreateVisit;

public class CreateVisitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CreateVisitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("visit")]
    public async Task<ActionResult<int>> Handle([FromBody] VisitRequest request)
    {
        var visit = new Domain.Aggregates.VisitAggregate.Visit(
            request.AnimalId, request.EmployeeId, request.Date, request.Type, request.VisitLength,
            request.VisitInformation
        );
        _dbContext.Visits.Add(visit);
        await _dbContext.SaveChangesAsync();

        return visit.Id;
    }
}