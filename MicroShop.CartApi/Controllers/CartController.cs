using MicroShop.CartApi.DTOs;
using MicroShop.CartApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.CartApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _repository;

    public CartController(ICartRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO checkoutDto)
    {
        var cart = await _repository.GetCartByUserIdAsync(checkoutDto.UserId);

        if (cart is null)
        {
            return NotFound($"Carrinho não encontrado para o usuário {checkoutDto.UserId}");
        }

        checkoutDto.CartItems = cart.CartItems;
        checkoutDto.DateTime = DateTime.Now;

        return Ok(checkoutDto);
    }

    [HttpPost("applycoupon")]
    public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDto)
    {
        var result = await _repository.ApplyCouponAsync(cartDto.CartHeader.UserId,
                                                        cartDto.CartHeader.CouponCode);

        if (!result)
        {
            return NotFound($"CartHeader não encontrado para o usuário {cartDto.CartHeader.UserId}");
        }
        return Ok(result);
    }

    [HttpDelete("deletecoupon/{userId}")]
    public async Task<ActionResult<CartDTO>> DeleteCoupon(string userId)
    {
        var result = await _repository.DeleteCouponAsync(userId);

        if (!result)
        {
            return NotFound($"Cupom de desconto não encontrado para o usuário {userId}");
        }

        return Ok(result);
    }

    [HttpGet("getcart/{userid}")]
    public async Task<ActionResult<CartDTO>> GetByUserId(string userid)
    {
        var cartDto = await _repository.GetCartByUserIdAsync(userid);

        if (cartDto is null)
            return NotFound();

        return Ok(cartDto);
    }


    [HttpPost("addcart")]
    public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDto)
    {
        var cart = await _repository.UpdateCartAsync(cartDto);

        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpPut("updatecart")]
    public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDto)
    {
        var cart = await _repository.UpdateCartAsync(cartDto);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("deletecart/{id}")]
    public async Task<ActionResult<bool>> DeleteCart(int id)
    {
        var status = await _repository.DeleteItemCartAsync(id);

        if (!status)
            return BadRequest();

        return Ok(status);
    }

}
