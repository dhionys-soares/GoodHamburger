using System.ComponentModel.DataAnnotations.Schema;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Domain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items => _items;
    
    public Guid Id { get; private set; }
    
    [NotMapped]
    public decimal SubTotal => _items.Sum(x => x.Total);

    public decimal Total { get; private set; }

    public Order()
    {
        Id = Guid.NewGuid();
    }

    public void AddItem(Product product)
    {
        if (product is null)
            throw new ProductCannotBeNullException();

        if (_items.Any(x => x.Product.Type == product.Type))
            throw new DuplicateProductException();
        
        _items.Add(new OrderItem(product));
    }

    public void RemoveItem(Guid productId)
    {
        var orderItem = _items.FirstOrDefault(x => x.Product.Id == productId);
        if (orderItem is null)
            throw new ProductNotFoundException();
        
        _items.Remove(orderItem);
    }

    public void UpdateItem(Guid productId)
    {
        var orderItem = _items.FirstOrDefault(x => x.Product.Id == productId);
        if (orderItem is null)
            throw new ProductNotFoundException();
        
        orderItem.Update(orderItem.Product);
    }
    
    public bool HasItemOfType(ProductType type)
    {
        return _items.Any(x => x.Product.Type == type);
    }

    public void ClearItems()
    {
        _items.Clear();
    }
    
    public void ApplyDiscount(Discount discount)
    {
        if (discount is null)
            throw new DiscountCannotBeNullException();
        
        Total = Math.Round(SubTotal - (SubTotal * discount.Value), 2,  MidpointRounding.AwayFromZero);
    }
}