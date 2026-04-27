using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces;

public interface IDiscountCalculator
{
    decimal CalculateDiscount(Order order);
}