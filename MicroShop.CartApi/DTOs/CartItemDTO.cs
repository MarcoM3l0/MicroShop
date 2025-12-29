namespace MicroShop.CartApi.DTOs;

public class CartItemDTO
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public ProductDTO? Product { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeaderDTO? CartHeader { get; set; }
}
