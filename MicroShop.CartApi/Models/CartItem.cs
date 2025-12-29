namespace MicroShop.CartApi.Models;

public class CartItem
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; } = 1;
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
    public Product? Product { get; set; }
    public CartHeader? CartHeader { get; set; }
}
