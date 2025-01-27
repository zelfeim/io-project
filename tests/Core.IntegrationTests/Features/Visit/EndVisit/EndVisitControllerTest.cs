using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Domain.Aggregates.AnimalAggregate;
using Application.Domain.Aggregates.AnimalOwnerAggregate;
using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Domain.Aggregates.VisitAggregate.Enums;
using Application.Features.Visit.EndVisit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Core.Tests.Features.Visit.EndVisit;

public class EndVisitControllerTest : BaseIntegrationTest
{
    public EndVisitControllerTest(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
        DbContext.AnimalOwners.Add(new AnimalOwner("Animal", "Owner", "email@email.com", "Olsztyn", "123456789"));
        DbContext.Animals.Add(new Animal(1, "Animal", "Species", "Race", 15));

        DbContext.Employees.Add(new Employee("EmployeeName", "EmployeeSurname", "Doctor", "EmployeeAddress"));

        DbContext.Visits.Add(new Application.Domain.Aggregates.VisitAggregate.Visit(1, 1, DateTime.Parse("2500-01-01"),
            VisitType.Examination, 30));
        DbContext.SaveChanges();
    }

    [Fact]
    public async Task EndVisit_ShouldSetVisitStatusToCompleted()
    {
        // Arrange 
        var request = new EndVisitRequest()
        {
            SuggestedTreatment = "Rest",
            PrescribedMeds = "SomeMed"
        };

        const int id = 1;

        // Act
        var response = await Client.PostAsJsonAsync($"api/visit/{id}/end", request,
            new JsonSerializerOptions()
                { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, Converters = { new JsonStringEnumConverter() } });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        DbContext.ChangeTracker.Clear();
        
        var visit = await DbContext.Visits.SingleAsync(v => v.Id == id);
        visit.VisitStatus.Should().Be(VisitStatus.Completed);
        visit.SuggestedTreatment.Should().Be("Rest");
        visit.Prescription!.PrescribedMeds.Should().Be("SomeMed");
    }
}