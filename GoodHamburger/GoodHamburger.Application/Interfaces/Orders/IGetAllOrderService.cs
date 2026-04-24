namespace GoodHamburger.Application.Interfaces.Orders;

public interface IGetAllOrderService
{
    Task<Response<List<Domain.Entities.Order>>>  GetAllOrdersAsync();
}