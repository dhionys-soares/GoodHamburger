namespace GoodHamburger.Web.Models;

public class OrderModel
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public List<OrderItemModel> Items { get; set; } = new();
}