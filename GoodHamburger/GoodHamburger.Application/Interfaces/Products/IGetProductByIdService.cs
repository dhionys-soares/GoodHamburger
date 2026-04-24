using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces.Products;

public interface IGetProductByIdService
{
    Task<Response<Product>> GetProductByIdAsync(ProductRequest productRequest);
}