using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.Employee.GetEmployee;

[ApiController]
[Route("api/employee")]
[Authorize(Roles = "Admin")]
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
    public async Task<ActionResult<List<GetEmployeeResponse>>> GetAll()
    {
        var employees = await _dbContext.Employees.ToListAsync();

        if (employees.Count == 0) return NotFound();

        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public ActionResult<GetEmployeeResponse> GetSingle(int id)
    {
        var employee = _dbContext.Employees.Find(id);

        if (employee == null) return NotFound();

        return Ok(employee);
    }
}