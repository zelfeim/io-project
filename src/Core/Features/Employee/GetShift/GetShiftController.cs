using Application.Features.Employee.NewShift;
using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Employee.GetShift;

[ApiController]
[Route("api/employee")]
public class GetShiftController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<NewShiftForEmployeeController> _logger;

    public GetShiftController(ApplicationDbContext dbContext, ILogger<NewShiftForEmployeeController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpGet("{id:int}/shift")]
    [Authorize(Roles = "Admin,Vet")]
    [SwaggerOperation(Tags = ["WorkSchedule"])]
    public async Task<ActionResult<IEnumerable<GetShiftResponse>>> GetShiftsForEmployeeByIdAsync(int id)
    {
        var employee = await _dbContext.Employees
            .Where(e => e.Id == id)
            .Include(e => e.WorkSchedule)
            .FirstOrDefaultAsync();

        if (employee == null) return NotFound();

        var shift = employee.WorkSchedule;

        return Ok(shift.Select(GetShiftMapper.MapWorkScheduleToGetShiftResponse));
    }
}