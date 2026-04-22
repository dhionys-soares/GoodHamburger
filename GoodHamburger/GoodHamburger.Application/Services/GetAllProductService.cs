using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services;

public class GetAllProductService : IGetAllProductService
{
    private readonly IProductRepository _productRepository;
    public GetAllProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Response<List<Product>?>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return products is null ? 
            Response<List<Product>?>.Fail("Products not found", "404") : 
            Response<List<Product>?>.Ok(products, "Products found");
    }
}