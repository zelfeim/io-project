using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Employee.DeleteShift;

[ApiController]
[Route("api/employee")]
public class DeleteShiftController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteShiftController> _logger;

    public DeleteShiftController(ILogger<DeleteShiftController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    // TODO make it better
    [HttpDelete("/shift/{id:int}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Tags = ["WorkSchedule"])]
    public async Task<ActionResult> DeleteShiftById(int id)
    {
        var employees = await _dbContext.Employees.Include(e => e.WorkSchedule).ToListAsync();

        var employee = employees.FirstOrDefault(e => e.WorkSchedule.Exists(s => s.Id == id));

        if (employee == null) return NotFound();

        var shift = employee.WorkSchedule.FirstOrDefault(s => s.Id == id);

        if (shift == null) return NotFound();

        employee.WorkSchedule.Remove(shift);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}