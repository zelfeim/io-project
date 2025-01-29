namespace Application.Features.Resources.GetResource;

public record GetResourceResponse
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public int Amount { get; init; }
    public DateTime ShelfLive { get; init; }
    public int Id { get; init; }
}