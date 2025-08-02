using FreemarketFx.ShoppingBasket.Application.Requests.Baskets.CreateBasket;
using FreemarketFx.ShoppingBasket.Application.Requests.Baskets.GetBasket;
using FreemarketFx.ShoppingBasket.Application.Requests.Baskets.SetDiscount;
using Microsoft.AspNetCore.Mvc;

namespace FreemarketFx.ShoppingBasket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController
(
    ICreateBasketHandler createBasketHandler,
    IGetBasketHandler getBasketHandler,
    ISetDiscountHandler setDiscountHandler
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateBasketResponse>> CreateBasket(CreateBasketRequest request)
    {
        var result = await createBasketHandler.CreateBasketAsync(request);
        return Created((string?)null, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        var result = await getBasketHandler.GetBasketAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPut("Discount")]
    public async Task<ActionResult> SetDiscount(SetDiscountRequest request)
    {
        var result = await setDiscountHandler.SetDiscountAsync(request);
        return result switch
        {
            SetDiscountResult.Success => NoContent(),
            SetDiscountResult.BasketNotFound => BadRequest("Basket ID not found"),
            SetDiscountResult.InvalidRequest => BadRequest("Discount Percent and Code must either both be set, or both be null"),
            _ => throw new InvalidOperationException("Invalid return value")
        };
    }
}
