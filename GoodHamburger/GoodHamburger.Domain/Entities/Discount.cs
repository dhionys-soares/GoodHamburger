using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Domain.Entities;

public class Discount : IDiscount
{
    public decimal CalculateDiscount(Order order)
    {
        bool hasSandwich = order.HasItemOfType(ProductType.Sandwich);
        bool hasFries = order.HasItemOfType(ProductType.Fries);
        bool hasDrink = order.HasItemOfType(ProductType.Drink);
        
        if (hasSandwich && hasFries && hasDrink)
            return 0.2m;

        if (hasSandwich && hasDrink)
            return 0.15m;

        if (hasSandwich && hasFries)
            return 0.10m;

        return 0m;
    }
}