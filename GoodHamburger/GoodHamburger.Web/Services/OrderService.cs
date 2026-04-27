using System.Net.Http.Json;
using GoodHamburger.Web.Models;
using GoodHamburger.Web.Responses;

namespace GoodHamburger.Web.Services;

public class OrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<OrderModel>> GetAllAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<ResponseModel<List<OrderModel>>>("api/orders");

        return response?.Data ?? new List<OrderModel>();
    }

    public async Task<OrderModel?> GetByIdAsync(Guid id)
    {
        var response = await _httpClient
            .GetFromJsonAsync<ResponseModel<OrderModel>>($"api/orders/{id}");

        return response?.Data;
    }

    public async Task<ResponseModel<OrderModel>?> CreateAsync(CreateOrderModel order)
    {
        var result = await _httpClient.PostAsJsonAsync("api/orders", order);
        return await result.Content.ReadFromJsonAsync<ResponseModel<OrderModel>>();
    }

    public async Task<bool> UpdateAsync(Guid id, CreateOrderModel order)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/orders/{id}", order);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"api/orders/{id}");
        return result.IsSuccessStatusCode;
    }
}