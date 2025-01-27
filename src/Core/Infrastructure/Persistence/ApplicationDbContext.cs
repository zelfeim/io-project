using System.Reflection;
using Application.Domain.Aggregates.AnimalAggregate;
using Application.Domain.Aggregates.AnimalOwnerAggregate;
using Application.Domain.Aggregates.EmployeeAggregate;
using Application.Domain.Aggregates.ResourcesAggregate;
using Application.Domain.Aggregates.VisitAggregate;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Visit> Visits { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalOwner> AnimalOwners { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Resources> Resources { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}