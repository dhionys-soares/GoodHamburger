using GoodHamburger.Application.Exceptions;
using GoodHamburger.Application.Interfaces.Orders;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Services.Orders;

public class CreateOrderService : ICreateOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDiscountCalculator _discountCalculator;
    private readonly IProductRepository _productRepository;
    
    public CreateOrderService(IOrderRepository orderRepository, IDiscountCalculator discountCalculator,  IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _discountCalculator = discountCalculator;
        _productRepository = productRepository;
    }

    public async Task<Response<Order>> CreateOrderAsync(OrderRequest request)
    {
        if (request is null)
            throw new RequestCannotBeNullException();
        
        if (!request.Items.Any())
            return Response<Order>.Fail("Items cannot be empty", "400");
        
        var order = new Order();
        var products = new List<Product>();

        foreach (var item in request.Items)
        {
            if (item.Quantity <= 0)
                return Response<Order>.Fail("Quantity must be greater than zero", "400");
            
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            if (product == null)
                return Response<Order>.Fail("Product not found", "400");
            
            products.Add(product);
        }
        
        var hasDuplicatedType = products
            .GroupBy(x => x.Type)
            .Any(x => x.Count() > 1);
        if (hasDuplicatedType)
            return Response<Order>.Fail("Order cannot have more than one product of the same type", "400");

        foreach (var itemRequest in request.Items)
        {
            var product = products.FirstOrDefault(x => x.Id == itemRequest.ProductId);
            order.AddItem(product);
        }
        
        
        var discount = new Discount(_discountCalculator.CalculateDiscount(order));
        order.ApplyDiscount(discount);
        
        await _orderRepository.AddOrderAsync(order);
        return Response<Order>.Ok(order, "Order created successfully");
    }
}