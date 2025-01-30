namespace Application.Features.Resources.GetResource;

public class GetResourceMapper
{
    public static GetResourceResponse MapResourceToGetResourceResponse(
        Domain.Aggregates.ResourcesAggregate.Resources resource)
    {
        return new GetResourceResponse
        {
            Id = resource.Id,
            Name = resource.Name,
            Type = resource.Type,
            Amount = resource.Amount,
            ShelfLive = resource.ShelfLive
        };
    }
}