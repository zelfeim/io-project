using System;
using System.Net;
using System.Threading.Tasks;
using Application.Domain.Aggregates.VisitAggregate.Enums;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Features.Visit.GetVisit;

public class GetVisitControllerTests : BaseIntegrationTest
{
    public GetVisitControllerTests(IntegrationTestWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        DbContext.Visits.Add(new Application.Domain.Aggregates.VisitAggregate.Visit(0, 0, DateTime.Parse("2500-01-01"),
            VisitType.Examination, 30));
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