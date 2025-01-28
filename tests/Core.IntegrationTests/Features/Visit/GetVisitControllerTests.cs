using System;
using System.Net;
using System.Threading.Tasks;
using Application.Domain.Aggregates.AnimalAggregate;
using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Domain.Aggregates.VisitAggregate.Enums;
using Application.Features.Visit.GetVisit;
using Core.Tests;
using FluentAssertions;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.IntegrationTests.Features.Visit;

public class GetVisitControllerTests : BaseIntegrationTest
{
    public GetVisitControllerTests(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
        DbContext.AnimalOwners.Add(
            new Application.Domain.Aggregates.AnimalOwnerAggregate.AnimalOwner("Animal", "Owner", "email@email.com",
                "Olsztyn", "123456789"));
        DbContext.Animals.Add(new Animal(1, "Animal", "Species", "Race", 15));

        DbContext.Employees.Add(new Application.Domain.Aggregates.EmployeeAggregate.Employee("EmployeeName",
            "EmployeeSurname", Role.Vet, "EmployeeAddress",
            "e@mail.com", string.Empty));

        DbContext.Visits.Add(new Application.Domain.Aggregates.VisitAggregate.Visit(1, 1, DateTime.Parse("2500-01-01"),
            VisitType.Examination, 30));
        DbContext.SaveChanges();
    }

    [Fact]
    public async Task GetVisit_ShouldReturnSpecifiedVisit()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await Client.GetAsync($"api/visit/{id}");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNull();

        var visit = JsonSerializer.Deserialize<GetVisitResponse>(content, JsonOptions);
        visit.AnimalId.Should().Be(1);
        visit.EmployeeId.Should().Be(1);
        visit.Date.Should().Be(DateTime.Parse("2500-01-01"));
        visit.VisitLength.Should().Be(30);
        visit.VisitType.Should().Be(VisitType.Examination);
        visit.VisitStatus.Should().Be(VisitStatus.Planned);
        visit.VisitInformation.Should().Be("");
        visit.SuggestedTreatment.Should().BeNull();
        visit.Prescription.Should().BeNull();
    }

    [Fact]
    public async Task GetVisit_ShouldReturnNotFound()
    {
        // Arrange
        const int id = 10;

        // Act
        var response = await Client.GetAsync($"api/visit/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}