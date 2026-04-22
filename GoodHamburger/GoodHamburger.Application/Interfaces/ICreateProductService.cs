using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface ICreateProductService
{
    Task<Response<Product>> CreateProductAsync(ProductRequest productRequest);
}