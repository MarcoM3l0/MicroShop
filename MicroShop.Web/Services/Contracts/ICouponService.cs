using MicroShop.Web.Models;

namespace MicroShop.Web.Services.Contracts;

public interface ICouponService
{
    Task<CouponViewModel> GetDiscountCoupon(string couponCode, string token);
}
