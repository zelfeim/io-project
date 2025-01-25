using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Features.Visit.CreateVisit;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace Core.Tests.Features.Visit.CreateVisit;

[TestSubject(typeof(CreateVisitController))]
public class CreateVisitControllerTest : BaseIntegrationTest
{
    protected CreateVisitControllerTest(IntegrationTestWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
    }

    [Fact]
    public async Task GetVisit_ShouldReturnExpectedVisit()
    {
        // Arrange
        var httpClient = new HttpClient(); 
        const int id = 1;
        
        // Act
        var visit = await Client.GetFromJsonAsync<Application.Domain.Aggregates.VisitAggregate.Visit>($"visit/{id}");

        // Assert
        visit.Should().NotBeNull();
    }
}