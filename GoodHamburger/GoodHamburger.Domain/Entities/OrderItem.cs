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

    public OrderItem(Product product)
    {
        if (product is null)
            throw new ProductCannotBeNullException();

        Product = product;
        Quantity = 1;
    }

    public void Update(Product product)
    {
        if (product is null)
            throw new ProductCannotBeNullException();

        Product = product;
        Quantity = 1;
    }
}