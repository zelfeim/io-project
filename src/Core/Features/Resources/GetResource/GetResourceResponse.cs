using Application.Domain.Aggregates.ResourcesAggregate;

namespace Application.Features.Resources.GetResource;

public record GetResourceResponse
{
    public required string Name { get; init; }
    public ResourceType Type { get; init; }
    public int Amount { get; init; }
    public DateTime ShelfLive { get; init; }
    public int Id { get; init; }
}