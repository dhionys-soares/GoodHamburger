using GoodHamburger.Application.Interfaces.Orders;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Services.Orders;

public class GetOrderByIdService : IGetOrderByIdService
{
    private readonly IOrderRepository _orderRepository;
    
    public GetOrderByIdService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Response<Order?>> GetOrderByIdAsync(Guid orderId)
    {
        if (orderId == Guid.Empty)
            return Response<Order?>.Fail("Invalid order id", "400");
        
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null)
            return Response<Order?>.Fail("Order not found", "404");
        
        return Response<Order?>.Ok(order, "Order found");
    }
}