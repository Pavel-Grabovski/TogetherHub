using Application.Data.DataBaseContext;
using Application.Security.Services;
using Infrastructure.Data.DataBaseContext;
using Infrastructure.Security.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString(
            "PostgreSQLConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddHttpContextAccessor();
        services.AddIdentityServices(configuration);
        services.AddScoped<IUserAccessor, UserAccessor>();
        return services;
    }
}
