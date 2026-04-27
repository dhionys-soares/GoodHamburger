using FluentAssertions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Services.Products;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Moq;

namespace GoodHamburger.Tests.Application.Products;

public class DeleteProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock = new();

    private DeleteProductService CreateService()
    {
        return new DeleteProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Product_Is_Not_Found()
    {
        var productId = Guid.NewGuid();

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        var service = CreateService();

        var response = await service.DeleteProductAsync(productId);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Product not found");
        response.Error.Should().Be("404");

        _productRepositoryMock.Verify(x => x.DeleteProductAsync(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task Should_Delete_Product_When_Product_Exists()
    {
        var product = new Product("X Burger", 5m, ProductType.Sandwich);

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(product.Id))
            .ReturnsAsync(product);

        var service = CreateService();

        var response = await service.DeleteProductAsync(product.Id);

        response.IsSucess.Should().BeTrue();
        response.Message.Should().Be("Product successfully deleted");
        response.Data.Should().Be(product);

        _productRepositoryMock.Verify(x => x.DeleteProductAsync(product.Id), Times.Once);
    }
}