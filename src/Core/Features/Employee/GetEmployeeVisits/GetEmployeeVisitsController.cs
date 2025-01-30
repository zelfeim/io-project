using Application.Features.Visit.GetVisit;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Features.Employee.GetEmployeeVisits;

[ApiController]
[Route("api/employee")]
public class GetEmployeeVisitsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetEmployeeVisitsController> _logger;

    public GetEmployeeVisitsController(ILogger<GetEmployeeVisitsController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}/visits")]
    [Authorize(Roles = "Admin,Vet,Receptionist")]
    public ActionResult<IEnumerable<GetVisitResponse>> GetEmployeeVisits([FromRoute] int id)
    {
        _logger.LogInformation("{Controller} called", nameof(GetEmployeeVisits));

        var visits = _dbContext.Visits.Where(v => v.Id == id).ToList();

        return Ok(visits.Select(GetVisitMapper.MapVisitToGetVisitResponse));
    }
}