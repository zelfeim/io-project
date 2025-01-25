using System;
using System.Net.Http;
using Application.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Protocol.Plugins;
using Xunit;

namespace Core.Tests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebApplicationFactory webApplicationFactory)
    {
        _scope = webApplicationFactory.Services.CreateScope();
        
        Client = webApplicationFactory.CreateClient();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
    
    public void Dispose()
    {
        _scope.Dispose(); 
        DbContext.Dispose();
    }
}