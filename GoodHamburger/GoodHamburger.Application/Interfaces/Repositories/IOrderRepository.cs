using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Domain.Entities.Order> AddOrderAsync(Domain.Entities.Order order);
    Task<Domain.Entities.Order?> GetOrderByIdAsync(Guid id);
    Task<Domain.Entities.Order?> UpdateOrderAsync(Domain.Entities.Order order);
    Task<bool> DeleteOrderAsync(Guid id);
    Task<List<Domain.Entities.Order>?> GetAllOrdersAsync();
}