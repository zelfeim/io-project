using Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Resources.AddResources;

[ApiController]
[Route("api/resource")]
public class CreateResourceController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CreateResourceController> _logger;

    public CreateResourceController(ILogger<CreateResourceController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Vet")]
    [SwaggerOperation(Tags = ["Resource"])]
    public async Task<ActionResult<int>> CreateResourceAsync([FromBody] CreateResourceRequest request)
    {
        var resource =
            new Domain.Aggregates.ResourcesAggregate.Resources(request.Name, request.Type, request.Amount,
                request.ShelfLife);

        _dbContext.Resources.Add(resource);
        await _dbContext.SaveChangesAsync();

        return Ok(resource.Id);
    }
}

public record CreateResourceRequest
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public int Amount { get; init; }
    public DateTime ShelfLife { get; init; }
}