using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application;

public class ProductRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductType Type { get; set; }
}