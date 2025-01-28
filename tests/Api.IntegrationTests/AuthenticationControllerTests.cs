using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Features.Employee.Authentication;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace Api.IntegrationTests;

[TestSubject(typeof(AuthenticationController))]
public class AuthenticationControllerTest : BaseIntegrationTest
{
    private const string Password = "password";

    public AuthenticationControllerTest(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
        var hashedPassword = SecretHasher.Hash(Password);

        DbContext.Employees.Add(
            new Employee("admin", "employee", Role.Admin, "address", "admin@employee.com", hashedPassword));
        DbContext.SaveChanges();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturnOk()
    {
        // Act
        var request = new LoginRequest
        {
            Email = "admin@employee.com",
            Password = Password
        };

        // Arrange
        var response = await Client.PostAsJsonAsync("login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var cookies = response.Headers.GetValues("Set-Cookie").ToList();
        cookies.Should().NotBeEmpty();
        cookies.Should().HaveCount(1);
        cookies[0].Should().StartWith(".AspNetCore.Cookies=");
    }

    [Fact]
    public async Task Login_With_Invalid_Credentials_Should_Return_Unauthorized()
    {
        // Act
        var request = new LoginRequest
        {
            Email = "invalid@email.com",
            Password = "password"
        };

        // Arrange
        var response = await Client.PostAsJsonAsync("login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact(Skip = "Cookie is saved and test passes successfully TOFIX")]
    public async Task ApiRequest_WithoutCookie_ShouldReturnUnauthorized()
    {
        // Act
        var response = await Client.GetAsync("api/employee/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ApiRequest_AfterLogin_ShouldReturnOk()
    {
        // Arrange
        const int id = 1;

        await Client.PostAsJsonAsync("login", new LoginRequest { Email = "", Password = "" });

        // Act
        var response = await Client.GetAsync($"api/employee/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeEmpty();
    }
}