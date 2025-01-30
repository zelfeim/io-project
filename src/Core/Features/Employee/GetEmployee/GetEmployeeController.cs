using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Employee.GetEmployee;

[ApiController]
[Route("api/employee")]
public class GetEmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetEmployeeController> _logger;

    public GetEmployeeController(ILogger<GetEmployeeController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Receptionist")]
    [SwaggerOperation(Tags = ["Employee"])]
    public async Task<ActionResult<List<GetEmployeeResponse>>> GetAll()
    {
        var employees = await _dbContext.Employees.ToListAsync();

        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Receptionist")]
    [SwaggerOperation(Tags = ["Employee"])]
    public ActionResult<GetEmployeeResponse> GetSingle(int id)
    {
        var employee = _dbContext.Employees.Find(id);

        if (employee == null) return NotFound();

        return Ok(employee);
    }
}