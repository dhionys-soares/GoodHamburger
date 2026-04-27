using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Services;

namespace GoodHamburger.Tests.Domain;

public class DiscountCalculatorTests
{
    [Fact]
    public void Should_Return_20_Percent_When_Order_Has_Sandwich_Fries_And_Drink()
    {
        var order = new Order();

        order.AddItem(new Product("X Burger", 5m, ProductType.Sandwich));
        order.AddItem(new Product("Fries", 2m, ProductType.Fries));
        order.AddItem(new Product("Drink", 2.5m, ProductType.Drink));

        var calculator = new DiscountCalculator();

        var discount = calculator.CalculateDiscount(order);

        discount.Should().Be(0.20m);
    }
    
    [Fact]
    public void Should_Return_15_Percent_When_Order_Has_Sandwich_And_Drink()
    {
        var order = new Order();

        order.AddItem(new Product("X Burger", 5m, ProductType.Sandwich));
        order.AddItem(new Product("Drink", 2.5m, ProductType.Drink));

        var calculator = new DiscountCalculator();

        var discount = calculator.CalculateDiscount(order);

        discount.Should().Be(0.15m);
    }
    
    [Fact]
    public void Should_Return_10_Percent_When_Order_Has_Sandwich_And_Fries()
    {
        var order = new Order();

        order.AddItem(new Product("X Burger", 5m, ProductType.Sandwich));
        order.AddItem(new Product("Fries", 2m, ProductType.Fries));

        var calculator = new DiscountCalculator();

        var discount = calculator.CalculateDiscount(order);

        discount.Should().Be(0.10m);
    }
    
    [Theory]
    [InlineData("X chicken", ProductType.Sandwich)]
    [InlineData("Tea", ProductType.Drink)]
    [InlineData("Crinckle", ProductType.Fries)]
    public void Should_Return_0_Percent_When_Order_Do_not_Match_with_Rules(string name, ProductType  productType)
    {
        var order = new Order();

        order.AddItem(new Product(name, 5m, productType));

        var calculator = new DiscountCalculator();

        var discount = calculator.CalculateDiscount(order);

        discount.Should().Be(0m);
    }
}