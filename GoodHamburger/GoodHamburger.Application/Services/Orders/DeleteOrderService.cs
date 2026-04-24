using GoodHamburger.Application.Interfaces.Orders;
using GoodHamburger.Application.Interfaces.Repositories;

namespace GoodHamburger.Application.Services.Orders;

public class DeleteOrderService : IDeleteOrderService
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Response<bool>> DeleteOrderAsync(Guid orderId)
    {
        if (orderId == Guid.Empty)
            return Response<bool>.Fail("Invalid order id", "400");
        
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null)
            return Response<bool>.Fail("Order not found", "404");
        
        var result = await _orderRepository.DeleteOrderAsync(order.Id);
        
        return result is true? 
            Response<bool>.Ok(true, "Order deleted successfully") : 
            Response<bool>.Fail("Could not delete order", "500");
    }
}