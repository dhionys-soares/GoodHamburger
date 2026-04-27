using FluentAssertions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Services.Orders;
using GoodHamburger.Domain.Entities;
using Moq;

namespace GoodHamburger.Tests.Application.Orders;

public class DeleteOrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();

    private DeleteOrderService CreateService()
    {
        return new DeleteOrderService(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Order_Id_Is_Empty()
    {
        var service = CreateService();

        var response = await service.DeleteOrderAsync(Guid.Empty);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Invalid order id");
        response.Error.Should().Be("400");
    }

    [Fact]
    public async Task Should_Return_Fail_When_Order_Is_Not_Found()
    {
        var orderId = Guid.NewGuid();

        _orderRepositoryMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync((Order?)null);

        var service = CreateService();

        var response = await service.DeleteOrderAsync(orderId);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Order not found");
        response.Error.Should().Be("404");
    }

    [Fact]
    public async Task Should_Return_Ok_When_Order_Is_Deleted()
    {
        var orderId = Guid.NewGuid();
        var order = new Order();

        _orderRepositoryMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(order);

        _orderRepositoryMock
            .Setup(x => x.DeleteOrderAsync(order.Id))
            .ReturnsAsync(true);

        var service = CreateService();

        var response = await service.DeleteOrderAsync(orderId);

        response.IsSucess.Should().BeTrue();
        response.Data.Should().BeTrue();
        response.Message.Should().Be("Order deleted successfully");
    }

    [Fact]
    public async Task Should_Return_Fail_When_Order_Could_Not_Be_Deleted()
    {
        var orderId = Guid.NewGuid();
        var order = new Order();

        _orderRepositoryMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(order);

        _orderRepositoryMock
            .Setup(x => x.DeleteOrderAsync(order.Id))
            .ReturnsAsync(false);

        var service = CreateService();

        var response = await service.DeleteOrderAsync(orderId);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Could not delete order");
        response.Error.Should().Be("500");
    }
}