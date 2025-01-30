using System.Security.Claims;
using Application.Features.Visit.GetVisit;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

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
    [Authorize(Roles = "Admin,Receptionist")]
    [SwaggerOperation(Tags = ["Employee"])]
    public ActionResult<IEnumerable<GetVisitResponse>> GetEmployeeVisits([FromRoute] int id)
    {
        _logger.LogInformation("{Controller} called", nameof(GetEmployeeVisits));

        var visits = _dbContext.Visits.Where(v => v.EmployeeId == id).ToList();

        return Ok(visits.Select(GetVisitMapper.MapVisitToGetVisitResponse));
    }

    [HttpGet("visits")]
    [SwaggerOperation(Tags = ["Employee"])]
    public ActionResult<IEnumerable<GetVisitResponse>> GetCurrentEmployeeVisits()
    {
        var id = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier);

        var visits = _dbContext.Visits.Where(v => v.EmployeeId == Convert.ToInt32(id)).ToList();

        return Ok(visits.Select(GetVisitMapper.MapVisitToGetVisitResponse));
    }
}