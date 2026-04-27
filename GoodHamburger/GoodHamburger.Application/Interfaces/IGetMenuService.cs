namespace GoodHamburger.Application.Interfaces;

public interface IGetMenuService
{
    Task<Response<MenuResponse>> GetMenuAsync();
}