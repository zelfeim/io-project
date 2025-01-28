namespace Application.Features.Employee.RegisterEmployee;

public static class RegisterEmployeeMapper
{
    public static Domain.Aggregates.EmployeeAggregate.Employee MapCreateEmployeeRequestToEmployee(
        CreateEmployeeRequest request)
    {
        return new Domain.Aggregates.EmployeeAggregate.Employee(request.Name, request.Surname, request.Role,
            request.Address, request.Email, request.Password);
    }
}