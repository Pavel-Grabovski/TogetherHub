namespace Application.Data.DataBaseContext;

public interface IApplicationDbContext
{
    public DbSet<Topic> Topics { get; }
    public DbSet<Relationship> Relationships { get; }
    public DbSet<User> Users { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken token);
}
