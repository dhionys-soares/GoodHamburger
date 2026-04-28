namespace GoodHamburger.Web.Models;

public class MenuModel
{
    public List<MenuItemModel> Sandwiches { get; set; } = new();
    public List<MenuItemModel> SideDish { get; set; } = new();
}