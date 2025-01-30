namespace Application.Domain.Aggregates.ResourcesAggregate;

public class Resources : IEntity, IAggregateRoot
{
    private Resources()
    {
    }

    public Resources(string name, string type, int amount, DateTime shelfLive)
    {
        Name = name;
        Type = type;
        Amount = amount;
        ShelfLive = shelfLive;
    }

    public string Name { get; }
    public string Type { get; }
    public int Amount { get; set; }
    public DateTime ShelfLive { get; }
    public int Id { get; }
}