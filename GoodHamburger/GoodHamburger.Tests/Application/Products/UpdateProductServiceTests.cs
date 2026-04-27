using FluentAssertions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Application.Services.Products;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Moq;

namespace GoodHamburger.Tests.Application.Products;

public class UpdateProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock = new();

    private UpdateProductService CreateService()
    {
        return new UpdateProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Request_Is_Null()
    {
        var service = CreateService();

        var response = await service.UpdateProductAsync(null!);

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
            Id = productId,
            Name = "X Burger",
            Price = 5m,
            Type = ProductType.Sandwich
        };

        var response = await service.UpdateProductAsync(request);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Product not found");
        response.Error.Should().Be("404");

        _productRepositoryMock.Verify(x => x.UpdateProductAsync(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task Should_Update_Product_When_Product_Exists()
    {
        var product = new Product("X Burger", 5m, ProductType.Sandwich);

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(product.Id))
            .ReturnsAsync(product);

        _productRepositoryMock
            .Setup(x => x.UpdateProductAsync(It.IsAny<Product>()))
            .ReturnsAsync((Product updatedProduct) => updatedProduct);

        var service = CreateService();

        var request = new ProductRequest
        {
            Id = product.Id,
            Name = "Refrigerante",
            Price = 2.5m,
            Type = ProductType.Drink
        };

        var response = await service.UpdateProductAsync(request);

        response.IsSucess.Should().BeTrue();
        response.Message.Should().Be("Product updated successfully");
        response.Data.Should().NotBeNull();
        response.Data!.Name.Should().Be("Refrigerante");
        response.Data.Price.Should().Be(2.5m);
        response.Data.Type.Should().Be(ProductType.Drink);

        _productRepositoryMock.Verify(x => x.UpdateProductAsync(product), Times.Once);
    }
}