using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> AddOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<Order?> UpdateOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(Guid id);
    Task<List<Order>?> GetAllOrdersAsync();
}