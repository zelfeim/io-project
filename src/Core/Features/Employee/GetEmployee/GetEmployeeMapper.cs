namespace Application.Features.Employee.GetEmployee;

public static class GetEmployeeMapper
{
    public static GetEmployeeResponse MapEmployeeToGetEmployeeResponse(
        Domain.Aggregates.EmployeeAggregate.Employee employee)
    {
        return new GetEmployeeResponse
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Email = employee.EmailAddress.Email,
            Role = employee.Role,
            Address = employee.Address
        };
    }
}