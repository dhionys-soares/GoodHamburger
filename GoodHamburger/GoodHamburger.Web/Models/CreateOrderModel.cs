namespace GoodHamburger.Web.Models;

public class CreateOrderModel
{
    public List<CreateOrderItemModel> Items { get; set; } = [];
}