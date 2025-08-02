using FreemarketFx.ShoppingBasket.Database;
using Microsoft.EntityFrameworkCore;

namespace FreemarketFx.ShoppingBasket.Application.Requests.BasketShippings.CreateOrUpdateBasketShipping;

internal class CreateOrUpdateBasketShippingHandler
(
    ShoppingBasketContext context
): ICreateOrUpdateBasketShippingHandler
{
    public async Task<CreateOrUpdateBasketShippingResponse> CreateOrUpdateBasketShippingAsync(Guid basketId, string countryCode, CreateOrUpdateBasketShippingRequest request)
    {
        var shipping = await context.BasketShippings.SingleOrDefaultAsync(bs => bs.BasketId == basketId && bs.CountryCode == countryCode);

        if (shipping == null)
        {
            shipping = new()
            {
                BasketId = basketId,
                CountryCode = countryCode
            };
            context.BasketShippings.Add(shipping);
        }

        shipping.ShippingCost = request.ShippingCost;

        await context.SaveChangesAsync();

        return new(shipping.BasketShippingId, shipping.CountryCode, shipping.ShippingCost);
    }
}
