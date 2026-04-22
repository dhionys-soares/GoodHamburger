using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services;

public class GetProductByIdService : IGetProductByIdService
{
    private readonly IProductRepository _productRepository;
    public GetProductByIdService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Response<Product>> GetProductByIdAsync(ProductRequest productRequest)
    {
        if (productRequest == null)
            return Response<Product>.Fail("Product cannot be null", "400");

        var product = await _productRepository.GetProductByIdAsync(productRequest.Id);
        
        if (product == null)
            return Response<Product>.Fail("Product cannot be null", "400");
        
        return Response<Product>.Ok(product, "Product got successfully");
    }
}