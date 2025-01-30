using Application.Domain.Aggregates.EmployeeAggregate;

namespace Application.Features.Employee.Authentication;

public interface IEmployeeValidationService
{
    bool ValidateCredentials(string email, string password);
    Role GetRole(string email);
    int GetId(string requestEmail);
}