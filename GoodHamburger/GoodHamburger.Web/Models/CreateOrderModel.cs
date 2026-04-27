namespace GoodHamburger.Web.Models;

public class CreateOrderModel
{
    public Guid Id { get; set; }
    public List<OrderItemModel> Items { get; set; } = [];
}