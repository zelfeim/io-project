using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    public ActionResult<List<GetEmployeeResponse>> Handle()
    {
        var employees = _dbContext.Employees.ToList();

        if (employees.Count == 0) return NotFound();

        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public ActionResult<GetEmployeeResponse> Handle(int id)
    {
        var employee = _dbContext.Employees.Find(id);

        if (employee == null) return NotFound();

        return Ok(employee);
    }
}