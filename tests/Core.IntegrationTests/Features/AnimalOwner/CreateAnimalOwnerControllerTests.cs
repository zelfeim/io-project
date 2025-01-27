using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Features.AnimalOwner.CreateAnimalOwner;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Core.Tests.Features.AnimalOwner;

public class CreateAnimalOwnerControllerTests : BaseIntegrationTest
{
    public CreateAnimalOwnerControllerTests(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
    }

    [Fact]
    public async Task CreateAnimalOwnerController_Should_Create_New_Animal_Owner()
    {
        // Act
        var request = new CreateAnimalOwnerRequest
        {
            Address = "Address",
            Email = "e@email.com",
            Name = "Name",
            Surname = "Surname",
            Telephone = "123456789"
        };

        // Arrange
        var response = await Client.PostAsJsonAsync("/api/animal-owner", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("1");
        var animalOwnerId = Convert.ToInt32(content);

        var animalOwner = await DbContext.AnimalOwners.Include(animalOwner => animalOwner.Animals)
            .SingleOrDefaultAsync(o => o.Id == animalOwnerId);

        animalOwner.Should().NotBeNull();
        animalOwner.Address.Should().Be("Address");
        animalOwner.Email.Email.Should().Be("e@email.com");
        animalOwner.Name.Should().Be("Name");
        animalOwner.Surname.Should().Be("Surname");
        animalOwner.Telephone.Should().Be("123456789");
        animalOwner.Animals.Should().HaveCount(0);
    }
}