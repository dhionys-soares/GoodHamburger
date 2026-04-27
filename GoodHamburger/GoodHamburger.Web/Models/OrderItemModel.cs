namespace GoodHamburger.Web.Models;

public class OrderItemModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}