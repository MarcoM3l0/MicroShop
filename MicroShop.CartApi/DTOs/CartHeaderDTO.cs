using System.ComponentModel.DataAnnotations;

namespace MicroShop.CartApi.DTOs;

public class CartHeaderDTO
{
    public int CartHeaderId { get; set; }
    [Required(ErrorMessage = "UserId is required")]
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
}
