using GoodHamburger.Application.Interfaces.Orders;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Services.Orders;

public class UpdateOrderService : IUpdateOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDiscount _discount;

    public UpdateOrderService(IOrderRepository orderRepository, IProductRepository productRepository,
        IDiscount discount)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _discount = discount;
    }

    public async Task<Response<Order>> UpdateOrderAsync(OrderRequest request)
    {
        if (!request.Items.Any())
            return Response<Order>.Fail("Items cannot be empty", "400");

        var order = await _orderRepository.GetOrderByIdAsync(request.Id);
        if (order == null)
            return Response<Order>.Fail("Order not found", "404");

        var products = new List<Product>();

        foreach (var itemRequest in request.Items)
        {
            if (itemRequest.Quantity <= 0)
                return Response<Order>.Fail("Quantity must be greater than zero", "400");

            var product = await _productRepository.GetProductByIdAsync(itemRequest.ProductId);
            if (product == null)
                return Response<Order>.Fail("Product not found", "404");

            products.Add(product);
        }

        var hasDuplicatedType = products
            .GroupBy(x => x.Type)
            .Any(x => x.Count() > 1);

        if (hasDuplicatedType)
            return Response<Order>.Fail("Order cannot have more than one product of the same type", "400");

        order.ClearItems();

        foreach (var itemRequest in request.Items)
        {
            var product = products.First(x => x.Id == itemRequest.ProductId);
            order.AddItem(product, itemRequest.Quantity);
        }

        var discount = _discount.CalculateDiscount(order);
        order.ApplyDiscount(discount);

        await _orderRepository.UpdateOrderAsync(order);
        return Response<Order>.Ok(order, "Order updated successfully");
    }
}