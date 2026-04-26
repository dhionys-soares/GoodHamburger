using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities;

public class OrderItem
{
    public Product Product { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal Total => Product.Price * Quantity;
    
    protected OrderItem()
    {
        
    }

    public OrderItem(Product product, int quantity)
    {
        if (product is null)
            throw new ProductCannotBeNullException();

        if (quantity <= 0)
            throw new InvalidQuantityException();

        Product = product;
        Quantity = quantity;
    }

    public void Update(Product product, int quantity)
    {
        if (product is null)
            throw new ProductCannotBeNullException();

        if (quantity <= 0)
            throw new InvalidQuantityException();

        Product = product;
        Quantity = quantity;
    }
}