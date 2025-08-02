using FreemarketFx.ShoppingBasket.Application.Requests.BasketShippings.CreateOrUpdateBasketShipping;
using Microsoft.AspNetCore.Mvc;

namespace FreemarketFx.ShoppingBasket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketShippingController
(
    ICreateOrUpdateBasketShippingHandler createOrUpdateBasketShippingHandler
): ControllerBase
{
    [HttpPut("{basketId}/{countryCode?}")]
    public async Task<ActionResult<CreateOrUpdateBasketShippingResponse>> CreateOrUpdateShipping(Guid basketId, CreateOrUpdateBasketShippingRequest request, string countryCode = "GB")
    {
        var result = await createOrUpdateBasketShippingHandler.CreateOrUpdateBasketShippingAsync(basketId, countryCode, request);

        return Created((string?)null, result);
    }
}
