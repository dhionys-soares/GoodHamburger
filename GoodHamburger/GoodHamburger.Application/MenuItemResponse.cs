using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Application;

public class MenuItemResponse
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}