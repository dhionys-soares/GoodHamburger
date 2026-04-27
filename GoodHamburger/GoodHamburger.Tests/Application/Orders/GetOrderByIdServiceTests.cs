using FluentAssertions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Services.Orders;
using GoodHamburger.Domain.Entities;
using Moq;

namespace GoodHamburger.Tests.Application.Orders;

public class GetOrderByIdServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();

    private GetOrderByIdService CreateService()
    {
        return new GetOrderByIdService(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Fail_When_Order_Id_Is_Empty()
    {
        var service = CreateService();

        var response = await service.GetOrderByIdAsync(Guid.Empty);

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

        var response = await service.GetOrderByIdAsync(orderId);

        response.IsSucess.Should().BeFalse();
        response.Message.Should().Be("Order not found");
        response.Error.Should().Be("404");
    }

    [Fact]
    public async Task Should_Return_Ok_When_Order_Is_Found()
    {
        var orderId = Guid.NewGuid();
        var order = new Order();

        _orderRepositoryMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(order);

        var service = CreateService();

        var response = await service.GetOrderByIdAsync(orderId);

        response.IsSucess.Should().BeTrue();
        response.Data.Should().Be(order);
        response.Message.Should().Be("Order found");
    }
}