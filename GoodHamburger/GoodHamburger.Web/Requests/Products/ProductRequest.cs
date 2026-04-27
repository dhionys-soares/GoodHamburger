namespace GoodHamburger.Web.Requests.Products;

public class ProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Type { get; set; }
}