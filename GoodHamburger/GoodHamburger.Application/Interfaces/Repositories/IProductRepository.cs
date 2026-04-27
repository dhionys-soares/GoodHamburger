using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<bool>  DeleteProductAsync(Guid id);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<List<Product>> GetAllProductsAsync();
}