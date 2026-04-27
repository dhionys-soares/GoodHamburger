namespace GoodHamburger.Application.Interfaces.Products;
using GoodHamburger.Domain.Entities;

public interface IGetAllProductService
{
    Task<Response<List<Product>>> GetAllProductsAsync();
}