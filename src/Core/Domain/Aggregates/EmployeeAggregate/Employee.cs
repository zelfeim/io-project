namespace Application.Domain.Aggregates.EmployeeAggregate;

public class Employee : IEntity, IAggregateRoot
{
    public string Name { get; }
    public string Surname { get; }
    public string Role { get; }
    public string Address { get; }
    public List<WorkSchedule> WorkSchedule { get; private set; }
    public int Id { get; }
}