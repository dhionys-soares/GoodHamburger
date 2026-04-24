using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces.Orders;

public interface ICreateOrderService
{
    Task<Response<Order>> CreateOrderAsync(OrderRequest request);
}