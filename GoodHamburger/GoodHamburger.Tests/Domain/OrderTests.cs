using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Should_Create_Order_With_Id_And_Empty_Items()
    {
        var order = new Order();

        order.Id.Should().NotBe(Guid.Empty);
        order.Items.Should().BeEmpty();
        order.SubTotal.Should().Be(0m);
        order.Total.Should().Be(0m);
    }

    [Fact]
    public void Should_Add_Item_When_Data_Is_Valid()
    {
        var order = new Order();
        var product = new Product("X Burger", 5m, ProductType.Sandwich);

        order.AddItem(product);

        order.Items.Should().HaveCount(1);
        order.SubTotal.Should().Be(5m);
    }

    [Fact]
    public void Should_Throw_Exception_When_Adding_Null_Product()
    {
        var order = new Order();

        var act = () => order.AddItem(null!);

        act.Should().Throw<ProductCannotBeNullException>();
    }

    [Fact]
    public void Should_Throw_Exception_When_Adding_Duplicated_Product_Type()
    {
        var order = new Order();

        var sandwich1 = new Product("X Burger", 5m, ProductType.Sandwich);
        var sandwich2 = new Product("X Salad", 6m, ProductType.Sandwich);

        order.AddItem(sandwich1);

        var act = () => order.AddItem(sandwich2);

        act.Should().Throw<DuplicateProductException>();
    }

    [Fact]
    public void Should_Remove_Item()
    {
        var order = new Order();
        var product = new Product("X Burger", 5m, ProductType.Sandwich);

        order.AddItem(product);

        order.RemoveItem(product.Id);

        order.Items.Should().BeEmpty();
        order.SubTotal.Should().Be(0m);
        order.Total.Should().Be(0m);
    }

    [Fact]
    public void Should_Throw_Exception_When_Removing_Nonexistent_Item()
    {
        var order = new Order();
        var productId = Guid.NewGuid();

        var act = () => order.RemoveItem(productId);

        act.Should()
            .Throw<ProductNotFoundException>();
    }

    [Fact]
    public void Should_Throw_Exception_When_Updating_Nonexistent_Item()
    {
        var order = new Order();
        var productId = Guid.NewGuid();

        var act = () => order.UpdateItem(productId);

        act.Should()
            .Throw<ProductNotFoundException>();;
    }

    [Fact]
    public void Should_Return_True_When_Order_Has_Item_Of_Type()
    {
        var order = new Order();
        var product1 = new Product("X Burger", 5m, ProductType.Sandwich);
        var product2 = new Product("Fries", 6m, ProductType.Fries);
        var product3 = new Product("Tea", 7m, ProductType.Drink);

        order.AddItem(product1);
        order.AddItem(product2);
        order.AddItem(product3);

        order.HasItemOfType(ProductType.Sandwich).Should().BeTrue();
        order.HasItemOfType(ProductType.Drink).Should().BeTrue();
        order.HasItemOfType(ProductType.Fries).Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_Order_Does_Not_Have_Item_Of_Type()
    {
        var order = new Order();

        order.HasItemOfType(ProductType.Drink).Should().BeFalse();
    }

    [Fact]
    public void Should_Clear_Items()
    {
        var order = new Order();
        var sandwich = new Product("X Burger", 5m, ProductType.Sandwich);
        var drink = new Product("Refrigerante", 2.5m, ProductType.Drink);

        order.AddItem(sandwich);
        order.AddItem(drink);

        order.ClearItems();

        order.Items.Should().BeEmpty();
        order.SubTotal.Should().Be(0m);
        order.Total.Should().Be(0m);
    }

    [Fact]
    public void Should_Apply_Discount_And_Round_Total()
    {
        var order = new Order();
        var sandwich = new Product("X Burger", 5m, ProductType.Sandwich);
        var drink = new Product("Refrigerante", 2.5m, ProductType.Drink);

        order.AddItem(sandwich);
        order.AddItem(drink);

        var discount = new Discount(0.15m);

        order.ApplyDiscount(discount);

        order.SubTotal.Should().Be(7.5m);
        order.Total.Should().Be(6.38m);
    }

    [Fact]
    public void Should_Apply_Zero_Discount()
    {
        var order = new Order();
        var sandwich = new Product("X Burger", 5m, ProductType.Sandwich);

        order.AddItem(sandwich);

        order.ApplyDiscount(new Discount(0m));

        order.Total.Should().Be(5m);
    }

    [Fact]
    public void Should_Throw_Exception_When_Discount_Is_Null()
    {
        var order = new Order();

        var act = () => order.ApplyDiscount(null!);

        act.Should().Throw<DiscountCannotBeNullException>();
    }
}