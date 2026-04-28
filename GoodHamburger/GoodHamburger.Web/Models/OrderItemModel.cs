namespace GoodHamburger.Web.Models;

public class OrderItemModel
{
    public ProductModel Product { get; set; } = new();
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}