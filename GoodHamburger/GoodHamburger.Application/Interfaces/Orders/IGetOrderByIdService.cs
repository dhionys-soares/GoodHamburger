using GoodHamburger.Domain.Entities;
namespace GoodHamburger.Application.Interfaces.Orders;

public interface IGetOrderByIdService
{
    Task<Response<Order?>> GetOrderByIdAsync(Guid orderId);
}