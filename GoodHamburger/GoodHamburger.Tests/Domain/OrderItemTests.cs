using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Tests.Domain;

public class OrderItemTests
{
    [Fact]
    public void Should_Create_OrderItem_When_Data_Is_Valid()
    {
        var product = new Product("X Burger", 5m, ProductType.Sandwich);

        var item = new OrderItem(product);

        item.Product.Should().Be(product);
        item.Quantity.Should().Be(1);
        item.Total.Should().Be(5m);
    }

    [Fact]
    public void Should_Throw_Exception_When_Product_Is_Null()
    {
        var act = () => new OrderItem(null!);

        act.Should().Throw<ProductCannotBeNullException>();
    }

    [Fact]
    public void Should_Update_OrderItem()
    {
        var sandwich = new Product("X Burger", 5m, ProductType.Sandwich);
        var drink = new Product("Refrigerante", 2.5m, ProductType.Drink);
        var item = new OrderItem(sandwich);

        item.Update(drink);

        item.Product.Should().Be(drink);
        item.Quantity.Should().Be(1);
        item.Total.Should().Be(2.5m);
    }

    [Fact]
    public void Should_Throw_Exception_When_Update_Product_Is_Null()
    {
        var product = new Product("X Burger", 5m, ProductType.Sandwich);
        var item = new OrderItem(product);

        var act = () => item.Update(null!);

        act.Should().Throw<ProductCannotBeNullException>();
    }
}