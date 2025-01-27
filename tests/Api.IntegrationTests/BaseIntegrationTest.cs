using System;
using System.Net.Http;
using Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;

    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebApplicationFactory webApplicationFactory)
    {
        _scope = webApplicationFactory.Services.CreateScope();

        Client = webApplicationFactory.CreateClient();
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