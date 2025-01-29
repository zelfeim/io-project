using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Employee.NewShift;

[ApiController]
[Route("api/employee")]
public class NewShiftForEmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<NewShiftForEmployeeController> _logger;

    public NewShiftForEmployeeController(ILogger<NewShiftForEmployeeController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost("{id:int}/shift")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Tags = ["WorkSchedule"])]
    public async Task<ActionResult<int>> NewShiftForEmployee(int id, [FromBody] NewShiftRequest request)
    {
        var employee = await _dbContext.Employees
            .Where(e => e.Id == id)
            .Include(e => e.WorkSchedule)
            .FirstOrDefaultAsync();

        if (employee == null) return NotFound();

        var shift = new WorkSchedule(request.Date, request.ShiftStart, request.ShiftEnd);

        employee.NewShift(shift);

        await _dbContext.SaveChangesAsync();

        return Ok(shift.Id);
    }
}