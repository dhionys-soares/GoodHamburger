using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces.Products;

public interface IUpdateProductService
{
    Task<Response<Product>> UpdateProductAsync(ProductRequest productRequest);
}