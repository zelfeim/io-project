using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Resources.UpdateResource;

[ApiController]
[Route("api/resource")]
public class UpdateResourceController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateResourceController> _logger;

    public UpdateResourceController(ILogger<UpdateResourceController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Vet")]
    [SwaggerOperation(Tags = ["Resource"])]
    public async Task<ActionResult> UpdateResourceAmount(int id, [FromBody] UpdateResourceRequest request)
    {
        var resource = await _dbContext.Resources.FindAsync(id);

        if (resource == null) return NotFound();

        resource.Amount = request.Amount;

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}