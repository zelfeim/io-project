using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Features.AnimalOwner;

public class DeleteAnimalOwnerControllerTests : BaseIntegrationTest
{
    public DeleteAnimalOwnerControllerTests(IntegrationTestWebApplicationFactory webApplicationFactory) : base(
        webApplicationFactory)
    {
        DbContext.AnimalOwners.Add(
            new Application.Domain.Aggregates.AnimalOwnerAggregate.AnimalOwner("name", "surname", "e@mail.com",
                "address", "123456789"));
        DbContext.SaveChanges();
    }

    [Fact]
    public async Task DeleteAnimalOwnerController_Should_Delete_Specified_AnimalOwner()
    {
        // Act  
        const int id = 1;

        // Arrange
        var response = await Client.DeleteAsync($"api/animal-owner/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}