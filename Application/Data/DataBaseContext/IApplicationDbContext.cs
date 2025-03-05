using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.DataBaseContext;

public interface IApplicationDbContext
{
    public DbSet<Topic> Topics { get; }
}
