using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Features.Employee.RegisterEmployee;

[ApiController]
[Route("api/employee")]
[Authorize(Roles = "Admin")]
public class RegisterEmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<RegisterEmployeeController> _logger;

    public RegisterEmployeeController(ILogger<RegisterEmployeeController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateEmployeeRequest request)
    {
        var employee = RegisterEmployeeMapper.MapCreateEmployeeRequestToEmployee(request);

        var existingEmployees = _dbContext.Employees.ToList();
        if (existingEmployees.Any(e => e.EmailAddress == employee.EmailAddress))
            return Conflict("Employee with this email address already exists");

        _dbContext.Add(employee);
        await _dbContext.SaveChangesAsync();

        return Ok(employee.Id);
    }
}