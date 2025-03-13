using Domain.Security;
using Infrastructure.Data.DataBaseContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        ApplicationDbContext dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        UserManager<CustomIdentityUser> manager = scope
            .ServiceProvider
            .GetRequiredService<UserManager<CustomIdentityUser>>();

        dbContext.Database
            .MigrateAsync()
            .GetAwaiter()
            .GetResult();

        await SeedData(dbContext, manager);
    }

    private static async Task SeedData(
        ApplicationDbContext dbContext,
        UserManager<CustomIdentityUser> userManager)
    {
        await SeedTopicAsync(dbContext);
        await SeedIdentityUsersAsync(dbContext, userManager);
    }


    private static async Task SeedTopicAsync(ApplicationDbContext dbContext)
    {
        if(!await dbContext.Topics.AnyAsync())
        {
            await dbContext.Topics.AddRangeAsync(InitialData.Topics);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedIdentityUsersAsync(
        ApplicationDbContext dbContext,
        UserManager<CustomIdentityUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            foreach (var user in InitialData.IdentityUsers) 
                await userManager.CreateAsync(user, "1111");
        }
    }
}
