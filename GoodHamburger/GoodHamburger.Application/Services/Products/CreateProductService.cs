using GoodHamburger.Application.Interfaces.Products;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services.Products;

public class CreateProductService : ICreateProductService
{
    private readonly IProductRepository _productRepository;

    public CreateProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Response<Product>> CreateProductAsync(ProductRequest productRequest)
    {
        if (productRequest == null)
            return Response<Product>.Fail("Product cannot be null", "400");
        
        if (string.IsNullOrWhiteSpace(productRequest.Name))
            return Response<Product>.Fail("Name is required", "400");

        if (productRequest.Price <= 0)
            return Response<Product>.Fail("Price must be greater than zero", "400");

        var product = new Product(productRequest.Name, productRequest.Price, productRequest.Type);
        await _productRepository.AddProductAsync(product);
        return Response<Product>.Ok(product, "Product created successfully");
    }
}