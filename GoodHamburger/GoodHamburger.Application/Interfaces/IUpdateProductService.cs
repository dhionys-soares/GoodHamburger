using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface IUpdateProductService
{
    Task<Response<Product>> UpdateProductAsync(ProductRequest productRequest);
}