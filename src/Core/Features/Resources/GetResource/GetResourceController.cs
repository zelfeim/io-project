using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Resources.GetResource;

[ApiController]
[Route("api/resource")]
public class GetResourceController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetResourceController> _logger;

    public GetResourceController(ILogger<GetResourceController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Vet,Receptionist")]
    [SwaggerOperation(Tags = ["Resource"])]
    public async Task<ActionResult<GetResourceResponse>> GetResourceById(int id)
    {
        var resource = await _dbContext.Resources.FindAsync(id);

        if (resource == null) return NotFound();

        return Ok(GetResourceMapper.MapResourceToGetResourceResponse(resource));
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Vet,Receptionist")]
    [SwaggerOperation(Tags = ["Resource"])]
    public async Task<ActionResult<IEnumerable<GetResourceResponse>>> GetResource()
    {
        var resources = await _dbContext.Resources.ToListAsync();

        return Ok(resources.Select(GetResourceMapper.MapResourceToGetResourceResponse));
    }
}