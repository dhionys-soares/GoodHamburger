using FluentAssertions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Services.Products;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Moq;

namespace GoodHamburger.Tests.Application.Products;

public class GetAllProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock = new();

    private GetAllProductService CreateService()
    {
        return new GetAllProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Ok_When_Products_Are_Found()
    {
        var products = new List<Product>
        {
            new("X Burger", 5m, ProductType.Sandwich),
            new("Refrigerante", 2.5m, ProductType.Drink)
        };

        _productRepositoryMock
            .Setup(x => x.GetAllProductsAsync())
            .ReturnsAsync(products);

        var service = CreateService();

        var response = await service.GetAllProductsAsync();

        response.IsSucess.Should().BeTrue();
        response.Message.Should().Be("Products found");
        response.Data.Should().HaveCount(2);
    }

    [Fact]
    public async Task Should_Return_Ok_When_Products_List_Is_Empty()
    {
        _productRepositoryMock
            .Setup(x => x.GetAllProductsAsync())
            .ReturnsAsync([]);

        var service = CreateService();

        var response = await service.GetAllProductsAsync();

        response.IsSucess.Should().BeTrue();
        response.Message.Should().Be("No products found");
        response.Data.Should().BeEmpty();
    }
}