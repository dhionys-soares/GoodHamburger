using GoodHamburger.Application.Interfaces.Products;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services.Products;

public class UpdateProductService : IUpdateProductService
{
    private readonly IProductRepository _productRepository;

    public UpdateProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public  async Task<Response<Product>> UpdateProductAsync(ProductRequest productRequest)
    {
        if (productRequest == null)
            return Response<Product>.Fail("Product cannot be null", "400");

        var product = await _productRepository.GetProductByIdAsync(productRequest.Id);
        if (product == null)
            return Response<Product>.Fail("Product not found", "404");
        
        product.Update(productRequest.Name, productRequest.Price, productRequest.Type);
        
        await _productRepository.UpdateProductAsync(product);
        return Response<Product>.Ok(product,  "Product updated successfully");
    }
}