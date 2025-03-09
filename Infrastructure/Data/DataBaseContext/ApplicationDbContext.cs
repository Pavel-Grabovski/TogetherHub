using Application.Data.DataBaseContext;
using Domain.Model;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data.DataBaseContext;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Topic> Topics
    {
        get => Set<Topic>();
    }

    public ApplicationDbContext(DbContextOptions options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly()
        );
    }
}
