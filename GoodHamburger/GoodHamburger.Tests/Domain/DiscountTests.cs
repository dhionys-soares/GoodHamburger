using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Tests.Domain;

public class DiscountTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(0.10)]
    [InlineData(0.15)]
    [InlineData(0.20)]
    public void Should_Create_Discount_When_Value_Is_Allowed(decimal value)
    {
        var discount = new Discount(value);

        discount.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(0.05)]
    [InlineData(0.25)]
    [InlineData(1)]
    public void Should_Throw_Exception_When_Discount_Value_Is_Out_Of_Range(decimal value)
    {
        var act = () => new Discount(value);

        act.Should().Throw<DiscountOutOfRangeAllowedException>();
    }
}