using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Domain.Entities;

public class Discount
{
    public decimal Value { get; private set;}
    
    public Discount(decimal value)
    {
        if (value is not (0m or 0.10m or 0.15m or 0.20m))
            throw new DiscountOutOfRangeAllowedException();

        Value = value;
    }
}