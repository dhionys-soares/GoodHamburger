using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface IProductService
{
    Task<Response<Product>> CreateProductAsync(ProductRequest productRequest);
    Task<Response<Product>> UpdateProductAsync(ProductRequest productRequest);
    Task<Response<Product>> DeleteProductAsync(ProductRequest productRequest);
    Task<Response<Product?>> GetProductByIdAsync(ProductRequest productRequest);
    Task<Response<List<Product>?>> GetAllProductsAsync();
}