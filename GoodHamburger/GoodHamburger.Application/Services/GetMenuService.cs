using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application.Services;

public class GetMenuService : IGetMenuService
{
    private readonly IProductRepository _productRepository;

    public GetMenuService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Response<MenuResponse>> GetMenuAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();

        if (products == null || !products.Any())
            return Response<MenuResponse>.Fail("No products found", "404");

        var menu = new MenuResponse
        {
            Sandwiches = products
                .Where(p => p.Type == ProductType.Sandwich)
                .Select(p => new MenuItemResponse
                {
                    Name = p.Name,
                    Price = p.Price
                }).ToList(),

            SideDish = products
                .Where(p => p.Type == ProductType.Fries || p.Type == ProductType.Drink)
                .Select(p => new MenuItemResponse
                {
                    Name = p.Name,
                    Price = p.Price
                }).ToList(),
        };

        return Response<MenuResponse>.Ok(menu, "Menu retrieved successfully");
    }
}