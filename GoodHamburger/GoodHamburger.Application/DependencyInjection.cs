using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Interfaces.Orders;
using GoodHamburger.Application.Interfaces.Products;
using GoodHamburger.Application.Services;
using GoodHamburger.Application.Services.Orders;
using GoodHamburger.Application.Services.Products;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateProductService, CreateProductService>();
        services.AddScoped<IUpdateProductService, UpdateProductService>();
        services.AddScoped<IDeleteProductService, DeleteProductService>();
        services.AddScoped<IGetProductByIdService, GetProductByIdService>();
        services.AddScoped<IGetAllProductService, GetAllProductService>();

        services.AddScoped<ICreateOrderService, CreateOrderService>();
        services.AddScoped<IUpdateOrderService, UpdateOrderService>();
        services.AddScoped<IDeleteOrderService, DeleteOrderService>();
        services.AddScoped<IGetOrderByIdService, GetOrderByIdService>();
        services.AddScoped<IGetAllOrderService, GetAllOrderService>();

        services.AddScoped<IGetMenuService, GetMenuService>();

        return services;
    }
}