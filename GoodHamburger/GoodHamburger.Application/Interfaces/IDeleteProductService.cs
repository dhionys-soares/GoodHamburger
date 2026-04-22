using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface IDeleteProductService
{
    Task<Response<Product>> DeleteProductAsync(Guid id);
}