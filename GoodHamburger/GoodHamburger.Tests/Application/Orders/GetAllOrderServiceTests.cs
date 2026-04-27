using FluentAssertions;
using GoodHamburger.Application.Interfaces.Repositories;
using GoodHamburger.Application.Services.Orders;
using GoodHamburger.Domain.Entities;
using Moq;

namespace GoodHamburger.Tests.Application.Orders;

public class GetAllOrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();

    private GetAllOrderService CreateService()
    {
        return new GetAllOrderService(_orderRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Return_Ok_With_Orders_When_Orders_Exist()
    {
        var orders = new List<Order>
        {
            new(),
            new()
        };

        _orderRepositoryMock
            .Setup(x => x.GetAllOrdersAsync())
            .ReturnsAsync(orders);

        var service = CreateService();

        var response = await service.GetAllOrdersAsync();

        response.IsSucess.Should().BeTrue();
        response.Data.Should().HaveCount(2);
        response.Message.Should().Be("Orders found");
    }

    [Fact]
    public async Task Should_Return_Ok_With_Empty_List_When_No_Orders_Exist()
    {
        _orderRepositoryMock
            .Setup(x => x.GetAllOrdersAsync())
            .ReturnsAsync([]);

        var service = CreateService();

        var response = await service.GetAllOrdersAsync();

        response.IsSucess.Should().BeTrue();
        response.Data.Should().BeEmpty();
        response.Message.Should().Be("No orders found");
    }

    [Fact]
    public async Task Should_Return_Ok_With_Empty_List_When_Repository_Returns_Null()
    {
        _orderRepositoryMock
            .Setup(x => x.GetAllOrdersAsync())
            .ReturnsAsync((List<Order>?)null);

        var service = CreateService();

        var response = await service.GetAllOrdersAsync();

        response.IsSucess.Should().BeTrue();
        response.Data.Should().BeEmpty();
        response.Message.Should().Be("No orders found");
    }
}