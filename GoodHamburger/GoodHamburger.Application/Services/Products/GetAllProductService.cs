using GoodHamburger.Application.Interfaces.Products;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services.Products;

public class GetAllProductService : IGetAllProductService
{
    private readonly IProductRepository _productRepository;
    public GetAllProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Response<List<Product>>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Response<List<Product>>.Ok(products, products.Any() ? "Products found" : "No products found");
    }
}