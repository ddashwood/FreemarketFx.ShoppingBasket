using FreemarketFx.ShoppingBasket.Application.Logic.Baskets;
using FreemarketFx.ShoppingBasket.Database;
using Microsoft.EntityFrameworkCore;

namespace FreemarketFx.ShoppingBasket.Application.Requests.Baskets.GetBasket;

internal class GetBasketHandler
(
    ShoppingBasketContext context,
    IBasketTotalCalculator basketTotalCalculator
) : IGetBasketHandler
{
    public async Task<GetBasketResponse?> GetBasketAsync(Guid basketId)
    {
        var basket = await context.Baskets
            .Include(b => b.BasketItems)
            .Include(b => b.BasketShippings)
            .AsSplitQuery()
            .SingleOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
        {
            return null;
        }

        return new (basket.BasketId,
                   basket.BasketName,
                   basketTotalCalculator.GetTotalWithoutVat(basket.BasketItems, basket.DiscountPercent ?? 0),
                   basketTotalCalculator.GetTotalWithVat(basket.BasketItems, basket.DiscountPercent ?? 0),
                   basket.BasketItems.Select(bi => new CreateBasketResponseItem(
                        bi.BasketItemId,
                        bi.Description,
                        bi.BasePricePerItem,
                        basketTotalCalculator.GetPricePerItemWithDiscount(bi, basket.DiscountPercent ?? 0),
                        bi.Quantity,
                        bi.DiscountPercent,
                        basketTotalCalculator.GetTotalWithoutVat([bi], basket.DiscountPercent ?? 0)
                    )).ToList(),
                    basket.DiscountPercent,
                    basket.DiscountCode,
                    basket.BasketShippings.Select(bs => new CreateBasketResponseShipping(
                        bs.BasketShippingId,
                        bs.CountryCode,
                        bs.ShippingCost
                    )).ToList()
                );
    }
}
