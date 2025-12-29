namespace MicroShop.Web.Models;

public class CartHeaderViewModel
{
    public int CartHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double TotalAmount { get; set; }
}
