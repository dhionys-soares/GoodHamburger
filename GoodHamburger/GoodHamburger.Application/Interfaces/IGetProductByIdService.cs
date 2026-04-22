using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface IGetProductByIdService
{
    Task<Response<Product>> GetProductByIdAsync(ProductRequest productRequest);
}