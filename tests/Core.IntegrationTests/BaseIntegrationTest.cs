using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Infrastructure.Persistence;
using Core.IntegrationTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Core.Tests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;

    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;

    protected readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    protected BaseIntegrationTest(IntegrationTestWebApplicationFactory webApplicationFactory)
    {
        _scope = webApplicationFactory.Services.CreateScope();

        Client = webApplicationFactory.CreateClient();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        DbContext.Database.Migrate();
    }

    public void Dispose()
    {
        _scope.Dispose();
        Client.Dispose();
        DbContext.Dispose();

        GC.SuppressFinalize(this);
    }
}