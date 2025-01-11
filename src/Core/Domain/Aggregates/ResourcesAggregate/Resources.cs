namespace Application.Domain.Aggregates.ResourcesAggregate;

public class Resources : IEntity, IAggregateRoot
{
    public string Name { get; }
    public string Type { get; }
    public int Amount { get; }
    public DateTime ShelfLive { get; }
    public int Id { get; }
}