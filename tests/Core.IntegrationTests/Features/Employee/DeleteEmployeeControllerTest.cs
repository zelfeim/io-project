using System.Net;
using System.Threading.Tasks;
using Application.Domain.Aggregates.EmployeeAggregate;
using Core.Tests;
using FluentAssertions;
using Xunit;

namespace Core.IntegrationTests.Features.Employee;

public class DeleteEmployeeControllerTest : BaseIntegrationTest
{
    public DeleteEmployeeControllerTest(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
        DbContext.Employees.Add(new Application.Domain.Aggregates.EmployeeAggregate.Employee("EmployeeName",
            "EmployeeSurname", Role.Vet, "EmployeeAddress",
            "e@mail.com", string.Empty));
        DbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task DeleteEmployee_Should_Delete_Existing_Employee()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await Client.DeleteAsync($"api/employee/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        DbContext.Employees.Should().HaveCount(0);
    }
}