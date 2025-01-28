using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Features.Employee.RegisterEmployee;
using Core.Tests;
using FluentAssertions;
using Xunit;

namespace Core.IntegrationTests.Features.Employee;

public class CreateEmployeeControllerTest : BaseIntegrationTest
{
    public CreateEmployeeControllerTest(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
    }

    [Fact]
    public async Task CreateEmployee_Should_Create_New_Employee()
    {
        // Arrange
        var request = new CreateEmployeeRequest
        {
            Name = "Name",
            Surname = "Surname",
            Email = "e@mail.com",
            Address = "Address",
            Password = "password",
            Role = Role.Vet
        };

        // Act
        var response = await Client.PostAsJsonAsync("api/employee", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeEmpty();

        var visitId = Convert.ToInt32(content);
        var employee = await DbContext.Employees.FindAsync(visitId);
        employee.Should().NotBeNull();
        employee!.Id.Should().Be(visitId);
        employee.Name.Should().Be(request.Name);
        employee.EmailAddress.Email.Should().Be(request.Email);
        employee.Surname.Should().Be(request.Surname);
        employee.Address.Should().Be(request.Address);
    }
}