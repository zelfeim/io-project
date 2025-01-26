using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Domain.Aggregates.VisitAggregate.Enums;
using Application.Features.Visit.CreateVisit;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace Core.Tests.Features.Visit.CreateVisit;

[TestSubject(typeof(CreateVisitController))]
public class CreateVisitControllerTest : BaseIntegrationTest
{
    public CreateVisitControllerTest(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
        DbContext.Visits.Add(new Application.Domain.Aggregates.VisitAggregate.Visit(0, 0, DateTime.Parse("2500-01-01"),
            VisitType.Examination, 30));
    }

    [Fact]
    public async Task GetVisit_ShouldReturnExpectedVisit()
    {
        // Arrange
        const int id = 1;
        
        // Act
        var visit = await Client.GetFromJsonAsync<Application.Domain.Aggregates.VisitAggregate.Visit>($"/api/visit/{id}");

        // Assert
        visit.Should().NotBeNull();
        visit.Id.Should().Be(id);
        visit.Date.Should().Be(DateTime.Now.AddDays(1));
        visit.VisitType.Should().Be(VisitType.Examination);
        visit.Date.Should().Be(DateTime.Parse("2500-01-01"));
    }
}