namespace GoodHamburger.Application;

public class MenuResponse
{
    public List<MenuItemResponse> Sandwiches { get; set; } = [];
    public List<MenuItemResponse> SideDish { get; set; } = [];
}