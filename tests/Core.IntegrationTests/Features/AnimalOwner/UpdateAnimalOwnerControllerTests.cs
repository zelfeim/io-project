using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Features.AnimalOwner.UpdateAnimalOwner;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Core.Tests.Features.AnimalOwner;

public class UpdateAnimalOwnerControllerTests : BaseIntegrationTest
{
    public UpdateAnimalOwnerControllerTests(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
        DbContext.AnimalOwners.Add(new Application.Domain.Aggregates.AnimalOwnerAggregate.AnimalOwner("name", "surname",
            "old@email.com", "OldAddress", "123456789"));
        DbContext.SaveChanges();
    }

    [Fact]
    public async Task UpdateAnimalOwnerController_Should_Update_Animal_Owner_With_New_Data()
    {
        // Act
        const int id = 1;
        var request = new UpdateAnimalOwnerRequest
        {
            Address = "NewAddress",
            Email = "new@email.com",
            Telephone = "987654321"
        };
        DbContext.ChangeTracker.Clear();

        // Arrange
        var response = await Client.PutAsJsonAsync($"/api/animal-owner/{id}/update", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        DbContext.AnimalOwners.Should().HaveCount(1);
        var animalOwner = await DbContext.AnimalOwners.SingleOrDefaultAsync(o => o.Id == id);
        animalOwner.Address.Should().Be("NewAddress");
        animalOwner.Email.Email.Should().Be("new@email.com");
        animalOwner.Telephone.Should().Be("987654321");
    }
}