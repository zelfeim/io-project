using Application.Domain.Aggregates.EmployeeAggregate;

namespace Application.Features.Employee.RegisterEmployee;

public record CreateEmployeeRequest
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public Role Role { get; init; }
    public string Address { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}