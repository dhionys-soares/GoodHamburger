using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Tests.Domain;

public class ProductTests
{
    [Fact]
    public void Should_Create_Product_When_Data_Is_Valid()
    {
        var product = new Product("X Burger", 5m, ProductType.Sandwich);

        product.Id.Should().NotBe(Guid.Empty);
        product.Name.Should().Be("X Burger");
        product.Price.Should().Be(5m);
        product.Type.Should().Be(ProductType.Sandwich);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_When_Name_Is_Invalid(string? name)
    {
        var act = () => new Product(name!, 5m, ProductType.Sandwich);

        act.Should().Throw<InvalidProductNameException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Throw_Exception_When_Price_Is_Invalid(decimal price)
    {
        var act = () => new Product("X Burger", price, ProductType.Sandwich);

        act.Should().Throw<InvalidProductPriceException>();
    }

    [Fact]
    public void Should_Update_Product()
    {
        var product = new Product("X Burger", 5m, ProductType.Sandwich);

        product.Update("Refrigerante", 2.5m, ProductType.Drink);

        product.Name.Should().Be("Refrigerante");
        product.Price.Should().Be(2.5m);
        product.Type.Should().Be(ProductType.Drink);
    }
}