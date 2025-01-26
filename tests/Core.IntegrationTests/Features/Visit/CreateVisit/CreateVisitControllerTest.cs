using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Domain.Aggregates.AnimalAggregate;
using Application.Domain.Aggregates.AnimalOwnerAggregate;
using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Domain.Aggregates.VisitAggregate.Enums;
using Application.Features.Visit.CreateVisit;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Core.Tests.Features.Visit.CreateVisit;

[TestSubject(typeof(CreateVisitController))]
public class CreateVisitControllerTest : BaseIntegrationTest
{
    public CreateVisitControllerTest(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
        DbContext.AnimalOwners.Add(new AnimalOwner("Animal", "Owner", "email@email.com", "Olsztyn", "123456789"));
        DbContext.Animals.Add(new Animal(1, "Animal", "Species", "Race", 15));

        DbContext.Employees.Add(new Employee("EmployeeName", "EmployeeSurname", "Doctor", "EmployeeAddress"));
        DbContext.SaveChanges();
    }

    [Fact]
    public async Task CreateVisit_ShouldCreateNewVisit()
    {
        // Arrange
        var createVisitRequest = new CreateVisitRequest()
        {
            AnimalId = 1,
            EmployeeId = 1,
            Type = VisitType.Examination,
            Date = Convert.ToDateTime("2500-01-01"),
            VisitInformation = "Some examination",
            VisitLength = 30
        };
        
        // Act
        var response = await Client.PostAsJsonAsync("api/visit", createVisitRequest, new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase, Converters = { new JsonStringEnumConverter() }});

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("1");
        var visitId = Convert.ToInt32(content);
        
        var visits = await DbContext.Visits.ToListAsync();
        visits.Should().HaveCount(1);
        visits.Should().Contain(v => v.Id == visitId);
        visits[0].AnimalId.Should().Be(1);
        visits[0].EmployeeId.Should().Be(1);
        visits[0].VisitType.Should().Be(VisitType.Examination);
        visits[0].Date.Should().Be(Convert.ToDateTime("2500-01-01"));
        visits[0].VisitInformation.Should().Be("Some examination");
        visits[0].VisitLength.Should().Be(30);
        visits[0].Prescription.Should().BeNull();
        visits[0].SuggestedTreatment.Should().BeNull();
    }
}