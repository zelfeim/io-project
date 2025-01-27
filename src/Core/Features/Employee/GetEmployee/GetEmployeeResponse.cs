using Application.Domain.Aggregates.EmployeeAggregate;

namespace Application.Features.Employee.GetEmployee;

public record GetEmployeeResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public Role Role { get; init; }
    public string Address { get; init; }
}