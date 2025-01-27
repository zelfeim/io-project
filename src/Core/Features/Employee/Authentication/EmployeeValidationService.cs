using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Employee.Authentication;

public class EmployeeValidationService : IEmployeeValidationService
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeValidationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool ValidateCredentials(string email, string password)
    {
        var employee = _dbContext.Employees.SingleOrDefaultAsync(e => e.EmailAddress.Email == email).Result;

        return employee != null && SecretHasher.Verify(employee.Password, password);
    }

    public Role GetRole(string email)
    {
        var employee = _dbContext.Employees.SingleOrDefaultAsync(e => e.EmailAddress.Email == email).Result;

        if (employee == null) throw new ArgumentException("Employee not found.");

        return employee.Role;
    }
}