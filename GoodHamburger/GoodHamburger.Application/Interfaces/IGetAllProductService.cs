using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces;

public interface IGetAllProductService
{
    Task<Response<List<Product>?>> GetAllProductsAsync();
}