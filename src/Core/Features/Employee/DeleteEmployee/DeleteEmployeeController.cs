using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Features.Employee.DeleteEmployee;

[ApiController]
[Route("api/employee")]
public class DeleteEmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteEmployeeController> _logger;

    public DeleteEmployeeController(ILogger<DeleteEmployeeController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")] 
    public async Task<ActionResult> Handle(int id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);

        if (employee == null) return NotFound();

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}