using GoodHamburger.Application.Interfaces.Orders;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Services.Orders;

public class CreateOrderService : ICreateOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDiscount _discount;
    private readonly IProductRepository _productRepository;
    
    public CreateOrderService(IOrderRepository orderRepository, IDiscount discount,  IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _discount = discount;
        _productRepository = productRepository;
    }

    public async Task<Response<Order>> CreateOrderAsync(OrderRequest request)
    {
        if (request == null)
            return Response<Order>.Fail("Order cannot be null", "400");

        var order = new Order(_discount);
        await _orderRepository.AddOrderAsync(order);

        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            
            if (product == null)
                return Response<Order>.Fail("Product not found", "400");
            
            order.AddItem(product, item.Quantity);
        }
        
        await _orderRepository.AddOrderAsync(order);
        return Response<Order>.Ok(order, "Order created successfully");
    }
}