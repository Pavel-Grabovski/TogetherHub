using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddExceptionHandler<ExtensionHandler>();
        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRouting(options => options.LowercaseUrls = true);

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(typeof(GetTopicsHandler).Assembly));
        return services;
    }

    public static WebApplication UseApiServices
        (this WebApplication app)
    {
        //app.UseMiddleware<ValidationMiddleware>();

        app.UseStatusCodePages(async context =>
        {
            if (context.HttpContext.Response.StatusCode == 403)
            {
                var detail = new ProblemDetails
                {
                    Title = "Forbidden",
                    Detail = "You do not have sufficient rights to perform this action.",
                    Status = StatusCodes.Status403Forbidden,
                    Instance = context.HttpContext.Request.Path
                };
                detail.Extensions.Add("TraceId:", context.HttpContext.TraceIdentifier);
                await context.HttpContext.Response.WriteAsJsonAsync(detail);
            }
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseExceptionHandler( options => { });
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        return app;
    }
}
