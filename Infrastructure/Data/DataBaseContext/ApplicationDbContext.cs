using Application.Data.DataBaseContext;
using Domain.Model;
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
}
