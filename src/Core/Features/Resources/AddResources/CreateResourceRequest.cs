using Application.Domain.Aggregates.ResourcesAggregate;

namespace Application.Features.Resources.AddResources;

public record CreateResourceRequest
{
    public required string Name { get; init; }
    public ResourceType Type { get; init; }
    public int Amount { get; init; }
    public DateTime ShelfLife { get; init; }
}