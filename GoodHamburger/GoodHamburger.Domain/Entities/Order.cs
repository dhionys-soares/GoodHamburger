using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Domain.Entities;

public class Order
{
    private readonly IDiscount _discount = null!;
    private readonly List<OrderItem> _items = new();
    
    public Guid Id { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;
    public decimal Total {
        get
        {
            var total = _items.Sum(x => x.Total);
            var discount = _discount.CalculateDiscount(this);
            return total - (total * discount);
        }
    }
    
    protected Order()
    {
    }

    public Order(IDiscount discount)
    {
        Id = Guid.NewGuid();
        _discount = discount ?? throw new DiscountCannotBeNullException();
    }

    public void AddItem(Product product, int quantity)
    {
        if (product is null)
            throw new ProductCannotBeNullException();

        if (quantity <= 0)
            throw new InvalidQuantityException();

        if (_items.Any(x => x.Product.Type == product.Type))
            throw new DuplicateProductException();
        
        _items.Add(new OrderItem(product, quantity));
    }

    public void RemoveItem(Guid productId)
    {
        var orderItem = _items.FirstOrDefault(x => x.Product.Id == productId);
        if (orderItem is null)
            throw new Exception($"Order item with id {productId} does not exist");
        
        _items.Remove(orderItem);
    }

    public void UpdateItem(Guid productId, int quantity)
    {
        var orderItem = _items.FirstOrDefault(x => x.Product.Id == productId);
        if (orderItem is null)
            throw new Exception($"Order item with id {productId} does not exist");
        
        orderItem.Update(orderItem.Product, quantity);
    }
    
    public bool HasItemOfType(ProductType type)
    {
        return _items.Any(x => x.Product.Type == type);
    }


    public void ClearItems()
    {
        _items.Clear();
    }
    
}