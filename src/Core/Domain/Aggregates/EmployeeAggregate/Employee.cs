using Application.Domain.Shared;

namespace Application.Domain.Aggregates.EmployeeAggregate;

public class Employee : IEntity, IAggregateRoot
{
    public string Name { get; }
    public string Surname { get; }
    public string Role { get; }
    public Address Address { get; }
    public WorkSchedule? WorkSchedule { get; private set; }
    public int Id { get; }
}