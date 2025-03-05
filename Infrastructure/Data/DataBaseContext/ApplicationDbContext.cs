using Application.Data.DataBaseContext;
using Domain.Model;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

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
        modelBuilder.Entity<Topic>()
            .Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => TopicId.Of(value)
            );

        modelBuilder.Entity<Topic>()
            .OwnsOne(topic => topic.Location, location =>
            {
                location.Property(l => l.City).HasColumnName("City");
                location.Property(l => l.Street).HasColumnName("Street");
            });
    }
}
