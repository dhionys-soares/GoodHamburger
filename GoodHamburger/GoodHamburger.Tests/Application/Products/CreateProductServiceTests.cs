using FluentAssertions;
using GoodHamburger.Application.Exceptions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Application.Services.Products;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Moq;

namespace GoodHamburger.Tests.Application.Products;

public class CreateProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock = new();

    private CreateProductService CreateService()
    {
        return new CreateProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Request_Is_Null()
    {
        var service = CreateService();

        var act = async () => await service.CreateProductAsync(null!);

        await act.Should().ThrowAsync<RequestCannotBeNullException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Should_Return_Fail_When_Name_Is_Invalid(string name)
    {
        var service = CreateService();

        var request = new ProductRequest
        {
            Name = name,
            Price = 5m,
            Type = ProductType.Sandwich
        };

        var response = await service.CreateProductAsync(request);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Name is required");
        response.Error.Should().Be("400");

        _productRepositoryMock.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Should_Return_Fail_When_Price_Is_Invalid(decimal price)
    {
        var service = CreateService();

        var request = new ProductRequest
        {
            Name = "X Burger",
            Price = price,
            Type = ProductType.Sandwich
        };

        var response = await service.CreateProductAsync(request);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Price must be greater than zero");
        response.Error.Should().Be("400");

        _productRepositoryMock.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task Should_Create_Product_When_Request_Is_Valid()
    {
        var service = CreateService();

        var request = new ProductRequest
        {
            Name = "X Burger",
            Price = 5m,
            Type = ProductType.Sandwich
        };

        _productRepositoryMock
            .Setup(x => x.AddProductAsync(It.IsAny<Product>()))
            .ReturnsAsync((Product product) => product);

        var response = await service.CreateProductAsync(request);

        response.IsSucess.Should().BeTrue();
        response.Message.Should().Be("Product created successfully");
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().NotBe(Guid.Empty);
        response.Data.Name.Should().Be("X Burger");
        response.Data.Price.Should().Be(5m);
        response.Data.Type.Should().Be(ProductType.Sandwich);

        _productRepositoryMock.Verify(x => x.AddProductAsync(It.IsAny<Product>()), Times.Once);
    }
}