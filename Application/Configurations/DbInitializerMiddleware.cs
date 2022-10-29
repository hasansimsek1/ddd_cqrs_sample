using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Configurations;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder InitializeDb(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DbInitializationMiddleware>();
    }
}

public class DbInitializationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDbInitializer _dbInitializer;
    private readonly IConfiguration _config;

    public DbInitializationMiddleware(RequestDelegate next, IConfiguration config, IDbInitializer dbInitializer)
    {
        _next = next;
        _dbInitializer = dbInitializer;
        _config = config;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!string.IsNullOrWhiteSpace(_config["isDbInitialized"]) &&
            _config["isDbInitialized"] == "true")
        {
            await _next(context);
        }
        else
        {
            await _dbInitializer.InitializeDbAsync();
            _config["isDbInitialized"] = "true";
        }
    }
}