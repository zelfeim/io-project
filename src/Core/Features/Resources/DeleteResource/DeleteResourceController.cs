using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Resources.DeleteResource;

[ApiController]
[Route("api/resource")]
public class DeleteResourceController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteResourceController> _logger;

    public DeleteResourceController(ILogger<DeleteResourceController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Vet")]
    [SwaggerOperation(Tags = ["Resource"])]
    public async Task<ActionResult> DeleteResource(int id)
    {
        var resource = await _dbContext.Resources.FindAsync(id);

        if (resource == null) return NotFound();

        _dbContext.Resources.Remove(resource);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}