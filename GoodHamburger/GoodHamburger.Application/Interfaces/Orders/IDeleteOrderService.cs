namespace GoodHamburger.Application.Interfaces.Orders;

public interface IDeleteOrderService
{
    Task<Response<bool>> DeleteOrderAsync(Guid orderId);
}