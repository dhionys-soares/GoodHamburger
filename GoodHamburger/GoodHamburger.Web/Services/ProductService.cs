using System.Net.Http.Json;
using GoodHamburger.Web.Models;
using GoodHamburger.Web.Requests.Products;
using GoodHamburger.Web.Responses;

namespace GoodHamburger.Web.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ResponseModel<List<ProductModel>>>("api/products");

        return response?.Data ?? new List<ProductModel>();
    }
    
    public async Task<ProductModel?> GetByIdAsync(Guid id)
    {
        var response = await _httpClient
            .GetFromJsonAsync<ResponseModel<ProductModel>>($"api/products/{id}");

        return response?.Data;
    }

    public async Task<ResponseModel<ProductModel>?> CreateAsync(CreateProductModel request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/products", request);
        return await result.Content.ReadFromJsonAsync<ResponseModel<ProductModel>>();
    }

    public async Task<bool> UpdateAsync(Guid id, ProductModel request)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/products/{id}", request);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _httpClient.DeleteAsync($"api/products/{id}");
        return result.IsSuccessStatusCode;
    }
}