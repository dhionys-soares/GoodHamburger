using GoodHamburger.Application.Interfaces.Orders;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services.Orders;

public class GetAllOrderService : IGetAllOrderService
{
    private readonly IOrderRepository _orderRepository;
    
    public GetAllOrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Response<List<Order>>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllOrdersAsync() ?? [];

        return Response<List<Order>>.Ok(orders, orders.Any() ? "Orders found" : "No orders found");
    }
}