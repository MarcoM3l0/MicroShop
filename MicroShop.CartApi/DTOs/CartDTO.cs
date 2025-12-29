namespace MicroShop.CartApi.DTOs;

public class CartDTO
{
    public CartHeaderDTO? CartHeader { get; set; }
    public List<CartItemDTO>? CartItems { get; set; }
}
