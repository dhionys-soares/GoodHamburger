using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IDiscountCalculator, DiscountCalculator>();
        return services;
    }
}