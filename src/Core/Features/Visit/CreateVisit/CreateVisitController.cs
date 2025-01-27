using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Visit.CreateVisit;

[ApiController]
[Route("api/visit")]
[Authorize(Roles = "Receptionist")]
public class CreateVisitController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CreateVisitController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Handle([FromBody] CreateVisitRequest request)
    {
        var visit = new Domain.Aggregates.VisitAggregate.Visit(
            request.AnimalId, request.EmployeeId, request.Date, request.Type, request.VisitLength,
            request.VisitInformation
        );
        _dbContext.Visits.Add(visit);
        await _dbContext.SaveChangesAsync();

        Created();
        return visit.Id;
    }
}