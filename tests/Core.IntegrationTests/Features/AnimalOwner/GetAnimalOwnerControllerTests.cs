using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Features.AnimalOwner.GetAnimalOwner;
using Core.Tests;
using FluentAssertions;
using Xunit;

namespace Core.IntegrationTests.Features.AnimalOwner;

public class GetAnimalOwnerControllerTests : BaseIntegrationTest
{
    public GetAnimalOwnerControllerTests(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
    }

    [Fact]
    public async Task GetAnimalOwnerController_Should_Return_Animal_Owner()
    {
        // Arrange 
        DbContext.AnimalOwners.Add(
            new Application.Domain.Aggregates.AnimalOwnerAggregate.AnimalOwner("name", "surname", "e@mail.com",
                "address", "123456789"));
        DbContext.SaveChanges();
        const int id = 1;

        // Act
        var response = await Client.GetAsync($"api/animal-owner/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();

        var animalOwnerResponse = JsonSerializer.Deserialize<GetAnimalOwnerResponse>(content, JsonOptions);

        animalOwnerResponse.Should().NotBeNull();
        animalOwnerResponse.Id.Should().Be(id);
        animalOwnerResponse.Address.Should().Be("address");
        animalOwnerResponse.Name.Should().Be("name");
        animalOwnerResponse.Surname.Should().Be("surname");
        animalOwnerResponse.Email.Email.Should().Be("e@mail.com");
        animalOwnerResponse.Telephone.Should().Be("123456789");
    }

    [Fact]
    public async Task GetAll_Should_Return_Empty_List()
    {
        // Act  
        var response = await Client.GetAsync("api/animal-owner");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();

        var animalOwnerResponse = JsonSerializer.Deserialize<List<GetAnimalOwnerResponse>>(content, JsonOptions);
        animalOwnerResponse.Should().BeEmpty();
    }
}