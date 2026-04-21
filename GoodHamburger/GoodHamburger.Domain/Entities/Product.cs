using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public ProductType Type { get; private set; }

    protected Product()
    {
    }

    public Product(string name, decimal price,  ProductType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidProductNameException();

        if (price <= 0)
            throw new InvalidProductPriceException();
        
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Type = type;
    }
}