using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces;

public interface IDiscount
{
    decimal CalculateDiscount(Order order);
}