using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
    }

    [Fact]
    public async Task CreateVisit_ShouldCreateNewVisit()
    {
        // Arrange
        var json = "";

        // Act
        var response = await Client.PostAsJsonAsync("api/visit", json);

        // Assert
        var visits = await DbContext.Visits.ToListAsync();
        visits.Should().HaveCount(1);
        visits.Should().Contain(v => v.Id == 1);
    }
}