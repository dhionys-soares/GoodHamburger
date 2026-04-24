namespace GoodHamburger.Application.Interfaces.Products;
using GoodHamburger.Domain.Entities;

public interface IDeleteProductService
{
    Task<Response<Product>> DeleteProductAsync(Guid id);
}