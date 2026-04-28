using System.Net.Http.Json;
using GoodHamburger.Web.Models;
using GoodHamburger.Web.Responses;

namespace GoodHamburger.Web.Services;

public class MenuService
{
    private readonly HttpClient _httpClient;

    public MenuService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MenuModel?> GetMenuAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ResponseModel<MenuModel>>("api/menu");

        return response?.Data;
    }
}