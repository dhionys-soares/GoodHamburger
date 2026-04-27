using FluentAssertions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Application.Services.Products;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Moq;

namespace GoodHamburger.Tests.Application.Products;

public class GetProductByIdServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock = new();

    private GetProductByIdService CreateService()
    {
        return new GetProductByIdService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Request_Is_Null()
    {
        var service = CreateService();

        var response = await service.GetProductByIdAsync(null!);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Product cannot be null");
        response.Error.Should().Be("400");
    }

    [Fact]
    public async Task Should_Return_Fail_When_Product_Is_Not_Found()
    {
        var productId = Guid.NewGuid();

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        var service = CreateService();

        var request = new ProductRequest
        {
            Id = productId
        };

        var response = await service.GetProductByIdAsync(request);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Product cannot be null");
        response.Error.Should().Be("400");
    }

    [Fact]
    public async Task Should_Return_Ok_When_Product_Is_Found()
    {
        var product = new Product("X Burger", 5m, ProductType.Sandwich);

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(product.Id))
            .ReturnsAsync(product);

        var service = CreateService();

        var request = new ProductRequest
        {
            Id = product.Id
        };

        var response = await service.GetProductByIdAsync(request);

        response.IsSucess.Should().BeTrue();
        response.Message.Should().Be("Product got successfully");
        response.Data.Should().Be(product);
    }
}