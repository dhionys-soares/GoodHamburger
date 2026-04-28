namespace GoodHamburger.Web.Models;

public class CreateProductModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Type { get; set; }
}