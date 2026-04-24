using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces.Products;

public interface ICreateProductService
{
    Task<Response<Product>> CreateProductAsync(ProductRequest productRequest);
}