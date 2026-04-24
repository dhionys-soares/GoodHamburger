namespace GoodHamburger.Application.Requests;

public class OrderRequest
{
    public Guid Id { get; set; }
    public List<OrderItemRequest> Items { get; set; } = [];
}