using FluentAssertions;
using GoodHamburger.Application.Exceptions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Requests;
using GoodHamburger.Application.Services.Orders;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Interfaces;
using Moq;

namespace GoodHamburger.Tests.Application.Orders;

public class CreateOrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
    private readonly Mock<IProductRepository> _productRepositoryMock = new();
    private readonly Mock<IDiscountCalculator> _discountCalculatorMock = new();

    private CreateOrderService CreateService()
    {
        return new CreateOrderService(
            _orderRepositoryMock.Object,
            _discountCalculatorMock.Object,
            _productRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Request_Is_Null()
    {
        var service = CreateService();

        var act = async () => await service.CreateOrderAsync(null!);

        await act.Should().ThrowAsync<RequestCannotBeNullException>().WithMessage("REQUEST_CANNOT_BE_NULL");
    }

    [Fact]
    public async Task Should_Return_Fail_When_Items_Are_Empty()
    {
        var service = CreateService();

        var request = new OrderRequest
        {
            Items = []
        };

        var response = await service.CreateOrderAsync(request);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Items cannot be empty");
        response.Error.Should().Be("400");
    }

    [Fact]
    public async Task Should_Return_Fail_When_Quantity_Is_Invalid()
    {
        var service = CreateService();

        var request = new OrderRequest
        {
            Items =
            [
                new OrderItemRequest
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 0
                }
            ]
        };

        var response = await service.CreateOrderAsync(request);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Quantity must be greater than zero");
        response.Error.Should().Be("400");

        _productRepositoryMock.Verify(x => x.GetProductByIdAsync(It.IsAny<Guid>()), Times.Never);
        _orderRepositoryMock.Verify(x => x.AddOrderAsync(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Product_Is_Not_Found()
    {
        var productId = Guid.NewGuid();

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        var service = CreateService();

        var request = new OrderRequest
        {
            Items =
            [
                new OrderItemRequest
                {
                    ProductId = productId,
                    Quantity = 1
                }
            ]
        };

        var response = await service.CreateOrderAsync(request);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Product not found");
        response.Error.Should().Be("400");

        _orderRepositoryMock.Verify(x => x.AddOrderAsync(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Order_Has_Duplicated_Product_Type()
    {
        var productId1 = Guid.NewGuid();
        var productId2 = Guid.NewGuid();

        var sandwich1 = new Product("X Burger", 5m, ProductType.Sandwich);
        var sandwich2 = new Product("X Salad", 6m, ProductType.Sandwich);

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(productId1))
            .ReturnsAsync(sandwich1);

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(productId2))
            .ReturnsAsync(sandwich2);

        var service = CreateService();

        var request = new OrderRequest
        {
            Items =
            [
                new OrderItemRequest { ProductId = productId1, Quantity = 1 },
                new OrderItemRequest { ProductId = productId2, Quantity = 1 }
            ]
        };

        var response = await service.CreateOrderAsync(request);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Order cannot have more than one product of the same type");
        response.Error.Should().Be("400");

        _orderRepositoryMock.Verify(x => x.AddOrderAsync(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task Should_Create_Order_When_Request_Is_Valid()
    {
        var sandwich = new Product("X Burger", 5m, ProductType.Sandwich);
        var drink = new Product("Refrigerante", 2.5m, ProductType.Drink);

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(sandwich.Id))
            .ReturnsAsync(sandwich);

        _productRepositoryMock
            .Setup(x => x.GetProductByIdAsync(drink.Id))
            .ReturnsAsync(drink);

        _discountCalculatorMock
            .Setup(x => x.CalculateDiscount(It.IsAny<Order>()))
            .Returns(0.15m);

        _orderRepositoryMock
            .Setup(x => x.AddOrderAsync(It.IsAny<Order>()))
            .ReturnsAsync((Order order) => order);

        var service = CreateService();

        var request = new OrderRequest
        {
            Items =
            [
                new OrderItemRequest { ProductId = sandwich.Id, Quantity = 1},
                new OrderItemRequest { ProductId = drink.Id, Quantity = 1 }
            ]
        };

        var response = await service.CreateOrderAsync(request);

        response.IsSucess.Should().BeTrue();
        response.Message.Should().Be("Order created successfully");
        response.Data.Should().NotBeNull();
        response.Data!.Items.Should().HaveCount(2);
        response.Data.SubTotal.Should().Be(7.5m);
        response.Data.Total.Should().Be(6.38m);

        _discountCalculatorMock.Verify(x => x.CalculateDiscount(It.IsAny<Order>()), Times.Once);
        _orderRepositoryMock.Verify(x => x.AddOrderAsync(It.IsAny<Order>()), Times.Once);
    }
}