using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces.Orders;

public interface IUpdateOrderService
{
    Task<Response<Order>> UpdateOrderAsync(OrderRequest request);
}