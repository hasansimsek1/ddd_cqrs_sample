using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistence.Postgres;
using Infrastructure.Persistence;
using Application.Contracts;
using Application.Services;
using Core.DomainModels.BasketModel;
using Infrastructure.Auth;

namespace Application.Configurations;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var appAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        services.AddMediatR(appAssemblies);
        services.AddAutoMapper(appAssemblies);
        services.AddHttpContextAccessor();
        services.AddSingleton<IDbInitializer, PostgresDbInitializer>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IStockService, StockService>();
    }
}